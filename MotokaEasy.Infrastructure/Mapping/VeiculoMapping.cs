using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Infrastructure.Mapping;

public class VeiculoMapping: IEntityTypeConfiguration<Veiculo>
{
    public void Configure(EntityTypeBuilder<Veiculo> builder)
    {
        builder.HasKey(key => new { key.Id });

        builder.Property(prop => prop.Id)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(prop => prop.Ano)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(prop => prop.Placa)
            .HasColumnType("varchar(8)")
            .IsRequired();
        
        builder.Property(prop => prop.ModeloVeiculoId)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(prop => prop.TipoVeiculoId)
            .HasColumnType("uuid")
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
        
        builder.HasOne(prop => prop.ModeloVeiculo)
            .WithMany(x => x.Veiculo)
            .HasForeignKey(prop => prop.ModeloVeiculoId);
        
        builder.HasOne(prop => prop.TipoVeiculo)
            .WithMany(x => x.Veiculo)
            .HasForeignKey(prop => prop.TipoVeiculoId);
    }
}