using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Domain.Entidade
{
    public class Cliente
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;

        public Guid EnderecoId { get; set; }

        #region relacionamento
        public Endereco? Endereco { get; set; }

        #endregion

    }
}
