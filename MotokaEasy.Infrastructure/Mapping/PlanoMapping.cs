using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Infrastructure.Mapping;

public class PlanoMapping: IEntityTypeConfiguration<Plano>
{
    public void Configure(EntityTypeBuilder<Plano> builder)
    {
        builder.HasKey(key => new { key.Id });

        builder.Property(prop => prop.Id)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(prop => prop.Descricao)
            .HasColumnType("varchar(200)")
            .IsRequired();
        
        builder.Property(prop => prop.Valor)
            .HasColumnType("float")
            .IsRequired();
        
        builder.Property(prop => prop.QuantidadeDias)
            .HasColumnType("int")
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