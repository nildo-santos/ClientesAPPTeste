using System;

namespace Clientes.Application.DTOs
{
  
    public class ClienteResponseDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Telefone { get; set; }

    
        public EnderecoResponseDTO Endereco { get; set; } = new EnderecoResponseDTO();
    }
}