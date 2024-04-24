using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Infrastructure.Mapping;

public class EntregadorMapping: IEntityTypeConfiguration<Entregador>
{
    public void Configure(EntityTypeBuilder<Entregador> builder)
    {
        builder.HasKey(key => new { key.Id });

        builder.Property(prop => prop.Id)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(prop => prop.CnpjCpf)
            .HasColumnType("varchar(14)")
            .IsRequired();
        
        builder.Property(prop => prop.Nome)
            .HasColumnType("varchar(150)")
            .IsRequired();
        
        builder.Property(prop => prop.DataNascimento)
            .HasColumnType("timestamp(0)")
            .IsRequired();

        builder.Property(prop => prop.NumeroCnh)
            .HasColumnType("varchar(11)")
            .IsRequired();
        
        builder.Property(prop => prop.TipoCnh)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(prop => prop.ImagemCnh)
            .HasColumnType("varchar(1000)");
        
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