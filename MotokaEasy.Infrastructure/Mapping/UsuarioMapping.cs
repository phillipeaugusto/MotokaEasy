using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Infrastructure.Mapping;

public class UsuarioMapping: IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(key => new { key.Id });
        
        builder.Property(prop => prop.Id)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(prop => prop.Nome)
            .HasColumnType("varchar(200)")
            .IsRequired();
        
        builder.Property(prop => prop.Email)
            .HasColumnType("varchar(200)")
            .IsRequired();
        
        builder.Property(prop => prop.Senha)
            .HasColumnType("varchar(2000)")
            .IsRequired();
        
        builder.Property(prop => prop.Numero)
            .HasColumnType("varchar(200)")
            .IsRequired();
        
        builder.Property(prop => prop.Role)
            .HasColumnType("varchar(50)")
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