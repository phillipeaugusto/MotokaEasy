using System;
using System.Threading;
using System.Threading.Tasks;
using MotokaEasy.Domain.Dto.OutPutDto;
using MotokaEasy.Domain.Entities;
using MotokaEasy.Domain.Repositories;
using static System.String;

namespace MotokaEasy.Domain.Business;

public class TotalizadorDiariaBusiness
{
    private readonly IConfiguracaoRepository _configuracaoRepository;
    private readonly IPlanoRepository _planoRepository;
    private readonly ILocacaoRepository _locacaoRepository;
    private readonly CancellationToken _cancellationToken;
    private string _parametroPlano7dias;
    private string _parametroPlano15dias;

    public TotalizadorDiariaBusiness(IConfiguracaoRepository configuracaoRepository, IPlanoRepository planoRepository, ILocacaoRepository locacaoRepository, CancellationToken cancellationToken)
    {
        _configuracaoRepository = configuracaoRepository;
        _planoRepository = planoRepository;
        _locacaoRepository = locacaoRepository;
        _cancellationToken = cancellationToken;
    }

    private double RetornarValorTotalDasDiarias(int quantidadeDiarias, double valorDiaria)
    {
        return quantidadeDiarias * valorDiaria;
    }

    private int RetornarDiariasNaoUtilizadas(DateTime dataDevolucao, DateTime dataPrevisao)
    {
        var diarias = dataDevolucao.Date - dataPrevisao.Date ;
        return (int)diarias.TotalDays * -1;
    }
    
    private int RetornarDiariasAdicionais(DateTime dataDevolucao, DateTime dataPrevisao)
    {
        if (dataDevolucao.Date < dataPrevisao.Date)
            return 0;
        
        var diarias = dataDevolucao.Date - dataPrevisao.Date; 
        return (int)diarias.TotalDays;
    }
    
    private int RetornarQuantidadeDeDiarias(DateTime dataInicial, DateTime dataDevolucao)
    {
        var diarias = dataDevolucao.Date - dataInicial.Date ;
        return (int)diarias.TotalDays;
    }

    private double RetornarValorPlano7dias(double valorPlano, int quantidadeDeDiariasNaoUtilizadas)
    {
        var totalBase = valorPlano * quantidadeDeDiariasNaoUtilizadas;
        var fatorAumento = 1 + (20.00 / 100);
        return totalBase * fatorAumento;
    }

    private double RetornarValorPlano15dias(double valorPlano, int quantidadeDeDiariasNaoUtilizadas)
    {
        var totalBase = valorPlano * quantidadeDeDiariasNaoUtilizadas;
        var fatorAumento = 1 + (40.00 / 100);
        return totalBase * fatorAumento;
    }

    private double RetornarTotalDeDiariasNaoUtilizadas(Plano plano, int quantidadeDeDiariasNaoUtilizadas)
    {
        var total = 0.00;

        if (IsNullOrEmpty(_parametroPlano7dias) && IsNullOrEmpty(_parametroPlano7dias))
            return total;

        if (_parametroPlano7dias is not null && plano.Id.ToString() == _parametroPlano7dias)
            total = RetornarValorPlano7dias(plano.Valor, quantidadeDeDiariasNaoUtilizadas);
        else if (_parametroPlano15dias is not null && plano.Id.ToString() == _parametroPlano15dias)
            total = RetornarValorPlano15dias(plano.Valor, quantidadeDeDiariasNaoUtilizadas);

        return total;
    }
    
    private double RetornarTotalDeDiariasAdicionais(int quantidadeDeDiariasAdicionais)
    {
        var total = 50.00 * quantidadeDeDiariasAdicionais;
        return total;
    }

    private bool ValidarSeDataDevolucaoMenorQueDataPrevisaoEntrega(DateTime dataDevolucao, DateTime dataPrevisao)
    {
       return dataDevolucao.Date < dataPrevisao.Date;
    }
    
    private bool ValidarSeDataDevolucaoMaiorQueDataPrevisaoEntrega(DateTime dataDevolucao, DateTime dataPrevisao)
    {
        return dataDevolucao.Date > dataPrevisao.Date;
    }
    
    public async Task<DetalhesTotalizadoresOutPutDto> RetornarValoresLocacaoDiarias(Guid locacaoId)
    {
        _parametroPlano7dias = await _configuracaoRepository.RetornarValorParametroAsync(Parameters.Parameters.Plano7DiasMulta20PorCento, _cancellationToken);
        _parametroPlano15dias = await _configuracaoRepository.RetornarValorParametroAsync(Parameters.Parameters.Plano15DiasMulta40PorCento, _cancellationToken);

        var valorTotalDasDiarias = 0.00;
        var valorDiariasNaoUtilizadas = 0.00;
        var valorDiariasAdicionais = 0.00;
        var quantidadeDeDiarias = 0;
        var quantidadeDeDiariasNaoUtilizadas = 0;
        var quantidadeDeDiariasAdicionais = 0;
        
        var objLocacao = await _locacaoRepository.GetByIdEntityAsync(locacaoId, _cancellationToken);
        var objPlano = await _planoRepository.GetByIdEntityAsync(objLocacao.PlanoId, _cancellationToken);

        quantidadeDeDiarias = RetornarQuantidadeDeDiarias(objLocacao.DataInicio, objLocacao.DataTerminio);
        valorTotalDasDiarias = RetornarValorTotalDasDiarias(quantidadeDeDiarias, objPlano.QuantidadeDias);

        if (ValidarSeDataDevolucaoMenorQueDataPrevisaoEntrega(objLocacao.DataTerminio, objLocacao.DataPrevisaoTerminio))
        {
            quantidadeDeDiariasNaoUtilizadas = RetornarDiariasNaoUtilizadas(objLocacao.DataTerminio, objLocacao.DataPrevisaoTerminio);
            valorDiariasNaoUtilizadas = RetornarTotalDeDiariasNaoUtilizadas(objPlano, quantidadeDeDiariasNaoUtilizadas);
        }

        if (ValidarSeDataDevolucaoMaiorQueDataPrevisaoEntrega(objLocacao.DataTerminio, objLocacao.DataPrevisaoTerminio))
        {
            quantidadeDeDiariasAdicionais = RetornarDiariasAdicionais(objLocacao.DataTerminio, objLocacao.DataPrevisaoTerminio);
            valorDiariasAdicionais = RetornarTotalDeDiariasAdicionais(quantidadeDeDiariasAdicionais);
        }

        var total = valorTotalDasDiarias + valorDiariasNaoUtilizadas + valorDiariasAdicionais;

        return new DetalhesTotalizadoresOutPutDto{ValorTotalDasDiarias = valorTotalDasDiarias, ValorDiariasNaoUtilizadas = valorDiariasNaoUtilizadas, ValorDiariasAdicionais = valorDiariasAdicionais, Total = total};
    }
}