using Clientes.Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clientes.Infra.Data.Mappings
{
    /// <summary>
    /// Classe de mapeamento para a entidade Cliente.
    /// </summary>
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("CLIENTES");

            builder.Property(c => c.Id).HasColumnName("ID");
            builder.Property(c=> c.Nome).HasColumnName("NOME").HasMaxLength(100).IsRequired();
            builder.Property(c => c.Email).HasColumnName("EMAIL").HasMaxLength(100).IsRequired();
            builder.Property(c => c.Telefone).HasColumnName("TELEFONE").HasMaxLength(15).IsRequired();
            builder.Property(c => c.EnderecoId).HasColumnName("ENDERECO_ID").IsRequired();

            builder.HasOne(c => c.Endereco)          
                .WithOne()                         
               .HasForeignKey<Cliente>(c => c.EnderecoId) 
               .OnDelete(DeleteBehavior.Restrict); 


        }
    }
}
