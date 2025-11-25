using Clientes.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clientes.Application.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteResponseDTO> AdicionarAsync(ClienteRequestDTO clienteDTO);

        Task<IEnumerable<ClienteResponseDTO>> ObterTodosAsync();
        Task<ClienteResponseDTO?> ObterPorIdAsync(Guid id);

        Task<ClienteResponseDTO> AtualizarAsync(Guid id, ClienteRequestDTO clienteDTO);

        Task DeletarAsync(Guid id);
    }
}