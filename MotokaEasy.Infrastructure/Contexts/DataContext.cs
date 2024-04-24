using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MotokaEasy.Core.ConstantsApp;
using MotokaEasy.Domain.Entities;
using MotokaEasy.Domain.Parameters;
using MotokaEasy.Infrastructure.Mapping;

namespace MotokaEasy.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var guidPlano7dias = Guid.NewGuid();
        var guidPlano15dias = Guid.NewGuid();
        
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .ApplyConfiguration(new EntregadorMapping())
            .ApplyConfiguration(new LocacaoMapping())
            .ApplyConfiguration(new ModeloVeiculoMapping())
            .ApplyConfiguration(new PlanoMapping())
            .ApplyConfiguration(new TipoVeiculoMapping())
            .ApplyConfiguration(new UsuarioMapping())
            .ApplyConfiguration(new ConfiguracaoMapping())
            .ApplyConfiguration(new VeiculoMapping());
        
        modelBuilder.Entity<Plano>().HasData(new List<Plano>
        {
            new() {Id = guidPlano7dias, Descricao = "7 dias", QuantidadeDias = 7, Valor = 30.00},
            new() {Id = guidPlano15dias, Descricao = "15 dias", QuantidadeDias = 15, Valor = 28.00},
            new() {Id = Guid.NewGuid(), Descricao = "30 dias", QuantidadeDias = 30, Valor = 22.00},
            new() {Id = Guid.NewGuid(), Descricao = "45 dias", QuantidadeDias = 45, Valor = 20.00},
            new() {Id = Guid.NewGuid(), Descricao = "50 dias", QuantidadeDias = 50, Valor = 18.00},
        });
        
        modelBuilder.Entity<TipoVeiculo>().HasData(new List<TipoVeiculo>
        {
            new() { Id = Guid.NewGuid(), Descricao = "Moto"},
            new() { Id = Guid.NewGuid(), Descricao = "Carro"}
        });
        
        modelBuilder.Entity<ModeloVeiculo>().HasData(new List<ModeloVeiculo>
        {
            new() { Id = Guid.NewGuid(), Descricao = "Honda Pop 100"},
            new() { Id = Guid.NewGuid(), Descricao = "Honda Nxr Bros 160"},
            new() { Id = Guid.NewGuid(), Descricao = "Honda CRF 250"},
            new() { Id = Guid.NewGuid(), Descricao = "Yamaha Fazer"},
            new() { Id = Guid.NewGuid(), Descricao = "Yamaha TTR"},
        });
        
        modelBuilder.Entity<Usuario>().HasData(new List<Usuario>
        {
            new(){ Id = Guid.NewGuid(), Nome = "UsuárioAdmUser", Email = "useradm@motokaeasy.com", Senha = "e0bc60c82713f64ef8a57c0c40d02ce24fd0141d5cc3086259c19b1e62a62bea", Role = RolesConstant.RoleAdmUser, Numero = "9999999997"},
            new(){ Id = Guid.NewGuid(), Nome = "UsuárioADM", Email = "adm@motokaeasy.com", Senha = "e0bc60c82713f64ef8a57c0c40d02ce24fd0141d5cc3086259c19b1e62a62bea", Role = RolesConstant.RoleAdministrator, Numero = "9999999998"},
            new(){ Id = Guid.NewGuid(), Nome = "UsuárioNormal", Email = "user@motokaeasy.com", Senha = "e0bc60c82713f64ef8a57c0c40d02ce24fd0141d5cc3086259c19b1e62a62bea", Role = RolesConstant.RoleUser, Numero = "9999999999"},
        });
        
        modelBuilder.Entity<Configuracao>().HasData(new List<Configuracao>
        {
            new() {Id = Guid.NewGuid(), CodigoParametro = Parameters.Plano7DiasMulta20PorCento, Descricao = "Guid do Plano de 7 dias multa de 20% sobre o valor das diárias não efetivadas", Valor = guidPlano7dias.ToString()},
            new() {Id = Guid.NewGuid(), CodigoParametro = Parameters.Plano15DiasMulta40PorCento, Descricao = "Guid do Plano de 15 dias multa de 40% sobre o valor das diárias não efetivadas", Valor = guidPlano15dias.ToString()},
        });
    }

    public DbSet<Entregador> Entregador { get; set; }
    public DbSet<Locacao> Locacao { get; set; }
    public DbSet<ModeloVeiculo> ModeloVeiculo { get; set; }
    public DbSet<Plano> Plano { get; set; }
    public DbSet<TipoVeiculo> TipoVeiculo { get; set; }
    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Veiculo> Veiculo { get; set; }
    public DbSet<Configuracao> Configuracao { get; set; }
}