using AutoMapper;
using Clientes.Application.DTOs;
using Clientes.Application.Interfaces;
using Clientes.Domain.Entidade;
using Clientes.Domain.Interfaces.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clientes.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        private void ValidarEmailUnico(string email, Guid? id = null)
        {
            
            var todosClientes = _clienteRepository.ConsultarTodos();

            var clienteExistente = todosClientes
                .FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

           
            if (clienteExistente != null && clienteExistente.Id != id)
            {
                throw new ApplicationException($"O e-mail '{email}' já está cadastrado.");
            }
        }

        public Task<ClienteResponseDTO> AdicionarAsync(ClienteRequestDTO clienteDTO)
        {
         
            ValidarEmailUnico(clienteDTO.Email);

          
            var cliente = _mapper.Map<Cliente>(clienteDTO);
            cliente.Id = Guid.NewGuid(); 
            cliente.Endereco.Id = Guid.NewGuid(); 

           
            _clienteRepository.Adicionar(cliente);

           
            return Task.FromResult(_mapper.Map<ClienteResponseDTO>(cliente));
        }

        public Task<IEnumerable<ClienteResponseDTO>> ObterTodosAsync()
        {
            var clientes = _clienteRepository.ConsultarTodos();
            return Task.FromResult(_mapper.Map<IEnumerable<ClienteResponseDTO>>(clientes));
        }

        public Task<ClienteResponseDTO?> ObterPorIdAsync(Guid id)
        {
            var cliente = _clienteRepository.ConsultarPorId(id);
            if (cliente == null)
            {
                return Task.FromResult<ClienteResponseDTO?>(null);
            }
            return Task.FromResult(_mapper.Map<ClienteResponseDTO>(cliente));
        }

        public Task<ClienteResponseDTO> AtualizarAsync(Guid id, ClienteRequestDTO clienteDTO)
        {
           
            ValidarEmailUnico(clienteDTO.Email, id);

           
            var clienteExistente = _clienteRepository.ConsultarPorId(id);
            if (clienteExistente == null)
            {
                throw new ApplicationException($"Cliente com ID '{id}' não encontrado para atualização.");
            }

           
            _mapper.Map(clienteDTO, clienteExistente);

           
            clienteExistente.Id = id;

          
            _clienteRepository.Atualizar(clienteExistente);

         
            return Task.FromResult(_mapper.Map<ClienteResponseDTO>(clienteExistente));
        }

        public Task DeletarAsync(Guid id)
        {
            var clienteExistente = _clienteRepository.ConsultarPorId(id);
            if (clienteExistente == null)
            {
                throw new ApplicationException($"Cliente com ID '{id}' não encontrado para exclusão.");
            }

            _clienteRepository.Deletar(id);
            return Task.CompletedTask;
        }
    }
}