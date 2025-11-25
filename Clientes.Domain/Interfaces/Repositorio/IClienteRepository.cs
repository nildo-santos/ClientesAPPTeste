using Clientes.Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Domain.Interfaces.Repositorio
{
    public interface IClienteRepository
    {
        void Adicionar(Cliente cliente);
        Cliente? ConsultarPorId(Guid id);
        IEnumerable<Cliente> ConsultarTodos();
        void Atualizar(Cliente cliente);
        void Deletar(Guid id);
    }
}
