using System.ComponentModel.DataAnnotations;

namespace Clientes.Application.DTOs
{
  
    public class EnderecoRequestDTO
    {
        [Required(ErrorMessage = "A rua é obrigatória.")]
        public string Rua { get; set; } = string.Empty;

        [Required(ErrorMessage = "O número é obrigatório.")]
        public string Numero { get; set; } = string.Empty;

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        public string Cidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "O estado é obrigatório.")]
        public string Estado { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        public string CEP { get; set; } = string.Empty;
    }
}