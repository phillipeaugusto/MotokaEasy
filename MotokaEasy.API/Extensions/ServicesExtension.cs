using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using MotokaEasy.Application.Services;
using MotokaEasy.Domain.Repositories;
using MotokaEasy.Infrastructure.Repositories;

namespace MotokaEasy.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class ServicesExtension
{
    public static void ServicesInitialization(this IServiceCollection services)
    {
        services.AddTransient<IEntregadorRepository, EntregadorRepository>();
        services.AddTransient<ILocacaoRepository, LocacaoRepository>();
        services.AddTransient<IModeloVeiculoRepository, ModeloVeiculoRepository>();
        services.AddTransient<IPlanoRepository, PlanoRepository>();
        services.AddTransient<ITipoVeiculoRepository, TipoVeiculoRepository>();
        services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        services.AddTransient<IVeiculoRepository, VeiculoRepository>();
        services.AddTransient<IConfiguracaoRepository, ConfiguracaoRepository>();

        services.AddTransient<ConfiguracaoService, ConfiguracaoService>();
        services.AddTransient<EntregadorService, EntregadorService>();
        services.AddTransient<LocacaoService, LocacaoService>();
        services.AddTransient<ModeloVeiculoService, ModeloVeiculoService>();
        services.AddTransient<PlanoService, PlanoService>();
        services.AddTransient<TipoVeiculoService, TipoVeiculoService>();
        services.AddTransient<UsuarioService, UsuarioService>();
        services.AddTransient<VeiculoService, VeiculoService>();
    }
}