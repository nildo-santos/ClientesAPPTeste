using Clientes.Domain.Entidade;
using Clientes.Domain.Interfaces.Repositorio;
using Clientes.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Infra.Data.Repositories
{
 
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly DataContext _dataContext;

      
        public EnderecoRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Adicionar(Endereco endereco)
        {
            _dataContext.Add(endereco);
            _dataContext.SaveChanges();
        }

     
        public void Atualizar(Endereco endereco)
        {
            _dataContext.Update(endereco);
            _dataContext.SaveChanges();
        }

      
        public Endereco? ConsultarPorId(Guid id)
        {
        
            return _dataContext.Set<Endereco>().FirstOrDefault(e => e.Id == id);
        }


        public IEnumerable<Endereco> ConsultarTodos()
        {
            return _dataContext.Set<Endereco>().ToList();
        }

        public void Deletar(Guid id)
        {
          
            var endereco = _dataContext.Set<Endereco>().Find(id);

            if (endereco != null)
            {
            
                _dataContext.Remove(endereco);
                _dataContext.SaveChanges();
            }
 
        }

    }
}
