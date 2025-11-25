using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Domain.Entidade
{
    public class Endereco
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Rua { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Cep { get; set; }= string.Empty;

        public Guid ClienteId { get; set; }

        public Cliente? Cliente { get; set; }
    }
}
 