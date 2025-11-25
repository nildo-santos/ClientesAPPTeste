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

        public ClienteServiceTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _mapperMock = new Mock<IMapper>();

            _service = new ClienteService(_clienteRepositoryMock.Object, _mapperMock.Object);

            // Configuração do Bogus para gerar dados fake
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
        }

        [Fact]
        public async Task DeveCriarClienteComSucesso()
        {
            var dto = _clienteFaker.Generate(); // gera dados fake automaticamente

            _clienteRepositoryMock.Setup(r => r.ConsultarTodos())
                .Returns(new List<Cliente>());

            Cliente clienteAdicionado = null!;
            _clienteRepositoryMock.Setup(r => r.Adicionar(It.IsAny<Cliente>()))
                .Callback<Cliente>(c => clienteAdicionado = c);

            var response = await _service.AdicionarAsync(dto);

            Assert.NotNull(response);
            Assert.Equal(dto.Nome, response.Nome);
            Assert.NotEqual(Guid.Empty, response.Id);
            Assert.NotNull(clienteAdicionado);
        }
    }
}
