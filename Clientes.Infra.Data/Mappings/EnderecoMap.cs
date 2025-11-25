using Clientes.Domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Infra.Data.Mappings
{
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("ENDERECO");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Rua)
                .HasColumnName("RUA")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Numero)
                .HasColumnName("NUMERO")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(e => e.Cidade).HasColumnName("CIDADE").HasMaxLength(25).IsRequired();
            builder.Property(e=> e.Estado).HasColumnName("ESTADO").HasMaxLength(25).IsRequired();
            builder.Property(e => e.Estado).HasColumnName("CEP").HasMaxLength(25).IsRequired();

        }
    }
}
