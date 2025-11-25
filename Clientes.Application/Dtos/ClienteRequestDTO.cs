using System.ComponentModel.DataAnnotations;

namespace Clientes.Application.DTOs
{
   
    public class ClienteRequestDTO
    {

        public Guid? Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public string Email { get; set; } = string.Empty;

        public string? Telefone { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public EnderecoRequestDTO Endereco { get; set; } = new EnderecoRequestDTO();
    }
}