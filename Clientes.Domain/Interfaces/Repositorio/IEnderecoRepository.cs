using Clientes.Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Domain.Interfaces.Repositorio
{
    public interface IEnderecoRepository
    {
        void Adicionar(Endereco endereco);

        Endereco? ConsultarPorId(Guid id);
        IEnumerable<Endereco> ConsultarTodos();

        void Atualizar(Endereco endereco);

        void Deletar(Guid id);
    }
}