using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Infrastructure.Mapping;

[ExcludeFromCodeCoverage]
public class ConfiguracaoMapping: IEntityTypeConfiguration<Configuracao>
{
    public void Configure(EntityTypeBuilder<Configuracao> builder)
    {
        builder.HasKey(key => new {key.Id});

        builder.Property(prop => prop.Id)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(prop => prop.CodigoParametro)
            .HasColumnType("int")
            .IsRequired();
        
        builder.Property(prop => prop.Descricao)
            .HasColumnType("varchar(150)")
            .IsRequired();
        
        builder.Property(prop => prop.Valor)
            .HasColumnType("varchar(1000)")
            .IsRequired();
            
        builder.Property(prop => prop.Status)
            .HasColumnType("varchar(1)")
            .IsRequired();

        builder.Property(prop => prop.DataCriacao)
            .HasColumnType("timestamp(0)")
            .IsRequired();
        
        builder.Property(prop => prop.DataAlteracao)
            .HasColumnType("timestamp(0)")
            .IsRequired();
        
    }
}