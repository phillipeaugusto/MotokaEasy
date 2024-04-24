using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Infrastructure.Mapping;

public class LocacaoMapping: IEntityTypeConfiguration<Locacao>
{
    public void Configure(EntityTypeBuilder<Locacao> builder)
    {
        builder.HasKey(key => new { key.Id });

        builder.Property(prop => prop.Id)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(prop => prop.EntregadorId)
            .HasColumnType("uuid")
            .IsRequired();
        
        builder.Property(prop => prop.PlanoId)
            .HasColumnType("uuid")
            .IsRequired();
        
        builder.Property(prop => prop.VeiculoId)
            .HasColumnType("uuid")
            .IsRequired();
        
        builder.Property(prop => prop.DataInicio)
            .HasColumnType("timestamp(0)")
            .IsRequired();
        
        builder.Property(prop => prop.DataTerminio)
            .HasColumnType("timestamp(0)");

        builder.Property(prop => prop.DataPrevisaoTerminio)
            .HasColumnType("timestamp(0)")
            .IsRequired();
        
        builder.Property(prop => prop.QuantidadeDeDiasDoPlano)
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
        
        builder.HasOne(prop => prop.Plano)
            .WithMany(x => x.Locacao)
            .HasForeignKey(prop => prop.PlanoId);

        builder.HasOne(prop => prop.Entregador)
            .WithMany(x => x.Locacao)
            .HasForeignKey(prop => prop.EntregadorId);
        
        builder.HasOne(prop => prop.Veiculo)
            .WithMany(x => x.Locacao)
            .HasForeignKey(prop => prop.VeiculoId);
    }
}