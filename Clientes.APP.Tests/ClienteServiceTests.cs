using AutoMapper;
using Bogus;
using Clientes.Application.DTOs;
using Clientes.Application.Mappers;
using Clientes.Application.Services;
using Clientes.Domain.Entidade;
using Clientes.Domain.Interfaces.Repositorio;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clientes.Tests
{
    public class ClienteServiceTests
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ClienteService _service;
        private readonly Faker<ClienteRequestDTO> _clienteFaker;

        private readonly Cliente _clienteEntidadeFake;
        private readonly ClienteResponseDTO _clienteResponseFake;
        private readonly List<Cliente> _clientesListaFake;


        public ClienteServiceTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _mapperMock = new Mock<IMapper>(); 

            _service = new ClienteService(_clienteRepositoryMock.Object, _mapperMock.Object);

            _clienteFaker = new Faker<ClienteRequestDTO>("pt_BR")
                .RuleFor(c => c.Nome, f => f.Person.FullName)
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.Telefone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Endereco, f => new EnderecoRequestDTO
                {
                    Rua = f.Address.StreetName(),
                    Numero = f.Address.BuildingNumber(),
                    Cidade = f.Address.City(),
                    Estado = f.Address.StateAbbr(),
                    CEP = f.Address.ZipCode()
                });

            _clienteEntidadeFake = new Cliente
            {
                Id = Guid.NewGuid(),
                Nome = "Base Cliente",
                Email = "base@teste.com",
                Endereco = new Endereco { Rua = "Rua Base", Cep = "00000-000" }
            };
            _clienteResponseFake = new ClienteResponseDTO
            {
                Id = _clienteEntidadeFake.Id,
                Nome = _clienteEntidadeFake.Nome,
                Email = _clienteEntidadeFake.Email
            };
            _clientesListaFake = new List<Cliente> { _clienteEntidadeFake, new Cliente { Id = Guid.NewGuid(), Email = "outro@teste.com" } };
        }

        [Fact(DisplayName = "Adicionar deve chamar Adicionar do Repositório e usar o Mapeador Mock")]
        public async Task DeveCriarClienteComSucesso()
        {

            var dto = _clienteFaker.Generate();


            _mapperMock.Setup(m => m.Map<Cliente>(It.IsAny<ClienteRequestDTO>())).Returns(new Cliente { Endereco = new Endereco() }); // Retorna entidade com Endereco inicializado

            _mapperMock.Setup(m => m.Map<ClienteResponseDTO>(It.IsAny<Cliente>())).Returns(_clienteResponseFake);

            _clienteRepositoryMock.Setup(r => r.ConsultarTodos()).Returns(new List<Cliente>());


            var response = await _service.AdicionarAsync(dto);

            Assert.NotNull(response);
            Assert.Equal(_clienteResponseFake.Nome, response.Nome);

            _clienteRepositoryMock.Verify(r => r.Adicionar(It.IsAny<Cliente>()), Times.Once);
            _mapperMock.Verify(m => m.Map<Cliente>(It.IsAny<ClienteRequestDTO>()), Times.Once);
            _mapperMock.Verify(m => m.Map<ClienteResponseDTO>(It.IsAny<Cliente>()), Times.Once);
        }

        [Fact(DisplayName = "Consultar por ID deve retornar DTO quando encontrado")]
        public async Task ConsultarPorId_DeveRetornarDTO()
        {

            _clienteRepositoryMock.Setup(r => r.ConsultarPorId(_clienteEntidadeFake.Id)).Returns(_clienteEntidadeFake);
            _mapperMock.Setup(m => m.Map<ClienteResponseDTO>(_clienteEntidadeFake)).Returns(_clienteResponseFake);

            var resultado = await _service.ObterPorIdAsync(_clienteEntidadeFake.Id);

            Assert.NotNull(resultado);
            Assert.Equal(_clienteResponseFake.Id, resultado.Id);
            _clienteRepositoryMock.Verify(r => r.ConsultarPorId(_clienteEntidadeFake.Id), Times.Once);
            _mapperMock.Verify(m => m.Map<ClienteResponseDTO>(_clienteEntidadeFake), Times.Once);
        }

        [Fact(DisplayName = "Consultar Todos deve retornar lista e usar o Mapeador")]
        public async Task ConsultarTodos_DeveRetornarLista()
        {

            _clienteRepositoryMock.Setup(r => r.ConsultarTodos()).Returns(_clientesListaFake);
            _mapperMock.Setup(m => m.Map<IEnumerable<ClienteResponseDTO>>(_clientesListaFake)).Returns(new List<ClienteResponseDTO> { _clienteResponseFake, _clienteResponseFake });


            var resultado = await _service.ObterTodosAsync();


            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _clienteRepositoryMock.Verify(r => r.ConsultarTodos(), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<ClienteResponseDTO>>(_clientesListaFake), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Cliente deve chamar Repositório com sucesso")]
        public async Task AtualizarCliente_DeveChamarAtualizarESucesso()
        {

            var dtoAtualizado = _clienteFaker.Generate();
            dtoAtualizado.Email = "novo.unico@teste.com";

            _clienteRepositoryMock.Setup(r => r.ConsultarPorId(_clienteEntidadeFake.Id)).Returns(_clienteEntidadeFake);

            _clienteRepositoryMock.Setup(r => r.ConsultarTodos()).Returns(new List<Cliente> { _clienteEntidadeFake });

            _mapperMock.Setup(m => m.Map(dtoAtualizado, _clienteEntidadeFake)).Returns(_clienteEntidadeFake);
            _mapperMock.Setup(m => m.Map<ClienteResponseDTO>(_clienteEntidadeFake)).Returns(_clienteResponseFake);

            var resultado = await _service.AtualizarAsync(_clienteEntidadeFake.Id, dtoAtualizado);

            Assert.NotNull(resultado);
            _clienteRepositoryMock.Verify(r => r.Atualizar(_clienteEntidadeFake), Times.Once, "O método Atualizar deve ser chamado com a entidade modificada.");
        }

        [Fact(DisplayName = "Remover Cliente deve chamar o método Deletar do Repositório")]
        public async Task RemoverCliente_DeveChamarDeletar()
        {

            _clienteRepositoryMock.Setup(r => r.ConsultarPorId(_clienteEntidadeFake.Id)).Returns(_clienteEntidadeFake);


            await _service.DeletarAsync(_clienteEntidadeFake.Id);

            _clienteRepositoryMock.Verify(r => r.Deletar(_clienteEntidadeFake.Id), Times.Once, "O método Deletar deve ser chamado uma vez com o ID correto.");
        }


        [Fact(DisplayName = "Falha: Criar Cliente deve lançar exceção se E-mail já existe")]
        public async Task CriarCliente_FalhaQuandoEmailDuplicado()
        {

            var dtoDuplicado = _clienteFaker.Generate();
            dtoDuplicado.Email = _clienteEntidadeFake.Email;

  
            _clienteRepositoryMock.Setup(r => r.ConsultarTodos()).Returns(_clientesListaFake);

            await Assert.ThrowsAsync<ApplicationException>(async () => await _service.AdicionarAsync(dtoDuplicado));

            _clienteRepositoryMock.Verify(r => r.Adicionar(It.IsAny<Cliente>()), Times.Never, "O repositório não deve ser chamado.");
        }


        [Fact(DisplayName = "Falha: Atualizar Cliente deve lançar exceção se novo E-mail já existe em outro cliente")]
        public async Task AtualizarCliente_FalhaQuandoEmailDuplicadoEmOutro()
        {

            var idClienteAtualizar = _clientesListaFake[0].Id; 
            var idClienteDuplicado = _clientesListaFake[1].Id; 

            var dtoDuplicado = _clienteFaker.Generate();
            dtoDuplicado.Email = _clientesListaFake[1].Email;

  
            _clienteRepositoryMock.Setup(r => r.ConsultarPorId(idClienteAtualizar)).Returns(_clientesListaFake[0]);
 
            _clienteRepositoryMock.Setup(r => r.ConsultarTodos()).Returns(_clientesListaFake);


            await Assert.ThrowsAsync<ApplicationException>(async () => await _service.AtualizarAsync(idClienteAtualizar, dtoDuplicado));

            _clienteRepositoryMock.Verify(r => r.Atualizar(It.IsAny<Cliente>()), Times.Never, "O repositório não deve ser chamado.");
        }


        [Fact(DisplayName = "Falha: Consultar por ID deve retornar null quando inexistente")]
        public async Task ConsultarPorId_FalhaQuandoInexistente()
        {

            var idInexistente = Guid.NewGuid();
            _clienteRepositoryMock.Setup(r => r.ConsultarPorId(idInexistente)).Returns((Cliente?)null);


            var resultado = await _service.ObterPorIdAsync(idInexistente);


            Assert.Null(resultado);
            _clienteRepositoryMock.Verify(r => r.ConsultarPorId(idInexistente), Times.Once);
        }

 
        [Fact(DisplayName = "Falha: Atualizar deve lançar exceção se cliente for inexistente")]
        public async Task AtualizarCliente_FalhaQuandoInexistente()
        {

            var idInexistente = Guid.NewGuid();
            var dto = _clienteFaker.Generate();
            _clienteRepositoryMock.Setup(r => r.ConsultarPorId(idInexistente)).Returns((Cliente?)null);

            await Assert.ThrowsAsync<ApplicationException>(async () => await _service.AtualizarAsync(idInexistente, dto));

            _clienteRepositoryMock.Verify(r => r.Atualizar(It.IsAny<Cliente>()), Times.Never, "O repositório não deve ser chamado.");
        }

        [Fact(DisplayName = "Falha: Deletar deve lançar exceção se cliente for inexistente")]
        public async Task RemoverCliente_FalhaQuandoInexistente()
        {
            var idInexistente = Guid.NewGuid();
            _clienteRepositoryMock.Setup(r => r.ConsultarPorId(idInexistente)).Returns((Cliente?)null);

            await Assert.ThrowsAsync<ApplicationException>(async () => await _service.DeletarAsync(idInexistente));

            _clienteRepositoryMock.Verify(r => r.Deletar(It.IsAny<Guid>()), Times.Never, "O repositório não deve ser chamado.");
        }
    }
}