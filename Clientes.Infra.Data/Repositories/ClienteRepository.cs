using Clientes.Domain.Entidade;
using Clientes.Domain.Interfaces.Repositorio;
using Clientes.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Infra.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DataContext _dataContext;

        public ClienteRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void Adicionar(Cliente cliente)
        {
            _dataContext.Add(cliente);
            _dataContext.SaveChanges();
        }

        public void Atualizar(Cliente cliente)
        {
            _dataContext.Update(cliente);
            _dataContext.SaveChanges();
        }

        public Cliente? ConsultarPorId(Guid id)
        {
            return _dataContext.Set<Cliente>()
                .Include(c => c.Endereco)
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);

        }

        public IEnumerable<Cliente> ConsultarTodos()
        {
           return _dataContext.Set<Cliente>()
                .Include(c => c.Endereco)
                .AsNoTracking()
                .ToList();


        }

        public void Deletar(Guid id)
        {
            var cliente = _dataContext.Set<Cliente>().Find(id);
            if ( cliente == null)
                throw new ApplicationException("Cliente não encontrado");

            _dataContext.Remove(cliente);
            _dataContext.SaveChanges();

            
        }
    }
}
