using Clientes.Domain.Entidade;
using Clientes.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;

namespace Clientes.Infra.Data.Contexts
{
    public class DataContext : DbContext
    {
   
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new EnderecoMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}