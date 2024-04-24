using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using MotokaEasy.Domain.Business;
using MotokaEasy.Domain.Entities;
using MotokaEasy.Domain.Parameters;
using MotokaEasy.Domain.Repositories;
using Xunit;
using static System.String;

namespace MotokaEasy.Tests.UnitaryTests.Business;

public class TotalizadorDiariaBusinessTests
{
    private static readonly Mock<IConfiguracaoRepository> MockConfiguracaoRepository = new();
    private static readonly Mock<IPlanoRepository> MockPlanoRepository = new();
    private static readonly Mock<ILocacaoRepository> MockLocacaoRepository = new();
    private readonly Guid _guidParametro7dias = Guid.NewGuid();
    private readonly Guid _guidParametro15dias = Guid.NewGuid();
    private readonly Guid _guidLocacao = Guid.NewGuid();
    

    public TotalizadorDiariaBusinessTests()
    {
    }

    [Fact]
    public async Task teste_validar_detalhes_diarias_locacao_sem_multa()
    {
        MockConfiguracaoRepository.Setup(x => x.RetornarValorParametroAsync(Parameters.Plano7DiasMulta20PorCento, It.IsAny<CancellationToken>())).ReturnsAsync(Empty);
        MockPlanoRepository.Setup(x => x.GetByIdEntityAsync(It.IsAny<Guid>(), new CancellationToken())).ReturnsAsync(new Plano { Id = _guidParametro7dias, Descricao = "Plano a", Valor = 10, QuantidadeDias = 10 });
        MockLocacaoRepository.Setup(x => x.GetByIdEntityAsync(It.IsAny<Guid>(), new CancellationToken())).ReturnsAsync(new Locacao {Id = _guidLocacao, DataInicio = DateTime.Now, DataTerminio = DateTime.Now.AddDays(2), DataPrevisaoTerminio = DateTime.Now.AddDays(2), PlanoId = _guidParametro7dias});
        
        var obj = await new TotalizadorDiariaBusiness(MockConfiguracaoRepository.Object, MockPlanoRepository.Object, MockLocacaoRepository.Object, new CancellationToken()).RetornarValoresLocacaoDiarias(_guidLocacao);
        Assert.Equal(20, obj.Total);
    }
    
    [Fact]
    public async Task teste_validar_detalhes_diarias_locacao_com_multa_plano7Dias()
    {
        MockConfiguracaoRepository.Setup(x => x.RetornarValorParametroAsync(Parameters.Plano7DiasMulta20PorCento, It.IsAny<CancellationToken>())).ReturnsAsync(_guidParametro7dias.ToString);
        MockPlanoRepository.Setup(x => x.GetByIdEntityAsync(It.IsAny<Guid>(), new CancellationToken())).ReturnsAsync(new Plano { Id = _guidParametro7dias, Descricao = "Plano 7 Dias", Valor = 10, QuantidadeDias = 7 });
        MockLocacaoRepository.Setup(x => x.GetByIdEntityAsync(It.IsAny<Guid>(), new CancellationToken())).ReturnsAsync(new Locacao {Id = _guidLocacao, DataInicio = DateTime.Now, DataTerminio = DateTime.Now.AddDays(2), DataPrevisaoTerminio = DateTime.Now.AddDays(7), PlanoId = _guidParametro7dias});
        
        var obj = await new TotalizadorDiariaBusiness(MockConfiguracaoRepository.Object, MockPlanoRepository.Object, MockLocacaoRepository.Object, new CancellationToken()).RetornarValoresLocacaoDiarias(_guidLocacao);
        Assert.Equal(74, obj.Total);
    }
    
    [Fact]
    public async Task teste_validar_detalhes_diarias_locacao_com_multa_plano15Dias()
    {
        MockConfiguracaoRepository.Setup(x => x.RetornarValorParametroAsync(Parameters.Plano7DiasMulta20PorCento, It.IsAny<CancellationToken>())).ReturnsAsync(_guidParametro15dias.ToString);
        MockPlanoRepository.Setup(x => x.GetByIdEntityAsync(It.IsAny<Guid>(), new CancellationToken())).ReturnsAsync(new Plano { Id = _guidParametro15dias, Descricao = "Plano 15 Dias", Valor = 10, QuantidadeDias = 15 });
        MockLocacaoRepository.Setup(x => x.GetByIdEntityAsync(It.IsAny<Guid>(), new CancellationToken())).ReturnsAsync(new Locacao {Id = _guidLocacao, DataInicio = DateTime.Now, DataTerminio = DateTime.Now.AddDays(2), DataPrevisaoTerminio = DateTime.Now.AddDays(7), PlanoId = _guidParametro15dias});
        
        var obj = await new TotalizadorDiariaBusiness(MockConfiguracaoRepository.Object, MockPlanoRepository.Object, MockLocacaoRepository.Object, new CancellationToken()).RetornarValoresLocacaoDiarias(_guidLocacao);
        Assert.Equal(90, obj.Total);
    }
    
    [Fact]
    public async Task teste_validar_detalhes_diarias_locacao_com_multa_plano7Dias_com_adicional()
    {
        MockConfiguracaoRepository.Setup(x => x.RetornarValorParametroAsync(Parameters.Plano7DiasMulta20PorCento, It.IsAny<CancellationToken>())).ReturnsAsync(_guidParametro7dias.ToString);
        MockPlanoRepository.Setup(x => x.GetByIdEntityAsync(It.IsAny<Guid>(), new CancellationToken())).ReturnsAsync(new Plano { Id = _guidParametro7dias, Descricao = "Plano 7 Dias", Valor = 10, QuantidadeDias = 7 });
        MockLocacaoRepository.Setup(x => x.GetByIdEntityAsync(It.IsAny<Guid>(), new CancellationToken())).ReturnsAsync(new Locacao {Id = _guidLocacao, DataInicio = DateTime.Now, DataTerminio = DateTime.Now.AddDays(8), DataPrevisaoTerminio = DateTime.Now.AddDays(7), PlanoId = _guidParametro7dias});

        var obj = await new TotalizadorDiariaBusiness(MockConfiguracaoRepository.Object, MockPlanoRepository.Object, MockLocacaoRepository.Object, new CancellationToken()).RetornarValoresLocacaoDiarias(_guidLocacao);
        Assert.Equal(106, obj.Total);
    }
    
    [Fact]
    public async Task teste_validar_detalhes_diarias_locacao_com_multa_plano15Dias_com_adicional()
    {
        MockConfiguracaoRepository.Setup(x => x.RetornarValorParametroAsync(Parameters.Plano15DiasMulta40PorCento, It.IsAny<CancellationToken>())).ReturnsAsync(_guidParametro15dias.ToString);
        MockPlanoRepository.Setup(x => x.GetByIdEntityAsync(It.IsAny<Guid>(), new CancellationToken())).ReturnsAsync(new Plano { Id = _guidParametro7dias, Descricao = "Plano 15 Dias", Valor = 10, QuantidadeDias = 15 });
        MockLocacaoRepository.Setup(x => x.GetByIdEntityAsync(It.IsAny<Guid>(), new CancellationToken())).ReturnsAsync(new Locacao {Id = _guidLocacao, DataInicio = DateTime.Now, DataTerminio = DateTime.Now.AddDays(8), DataPrevisaoTerminio = DateTime.Now.AddDays(7), PlanoId = _guidParametro15dias});

        var obj = await new TotalizadorDiariaBusiness(MockConfiguracaoRepository.Object, MockPlanoRepository.Object, MockLocacaoRepository.Object, new CancellationToken()).RetornarValoresLocacaoDiarias(_guidLocacao);
        Assert.Equal(170, obj.Total);
    }
}