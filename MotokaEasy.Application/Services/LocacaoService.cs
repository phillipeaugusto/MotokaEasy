using System.Threading;
using System.Threading.Tasks;
using Flunt.Notifications;
using MotokaEasy.Core.Commands;
using MotokaEasy.Core.Messages.Shared;
using MotokaEasy.Core.Services.Contracts;
using MotokaEasy.Core.Shared.Contracts;
using MotokaEasy.Core.Shared.Result;
using MotokaEasy.Domain.Business;
using MotokaEasy.Domain.Command;
using MotokaEasy.Domain.Enum;
using MotokaEasy.Domain.Repositories;
using static System.String;

namespace MotokaEasy.Application.Services;

public class LocacaoService:
    Notifiable,
    IService<CriarLocacaoCommand>,
    IService<InformarDataDevolucaoLocacaoCommand>,
    IService<ObterDetalhesValoresLocacaoCommand>,
    IService<GetAllPaginationCommand>
{
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly ILocacaoRepository _locacaoRepository;
    private readonly IEntregadorRepository _entregadorRepository;
    private readonly IPlanoRepository _planoRepository;
    private readonly IConfiguracaoRepository _configuracaoRepository;

    public LocacaoService(IVeiculoRepository veiculoRepository, ILocacaoRepository locacaoRepository, IEntregadorRepository entregadorRepository, IPlanoRepository planoRepository, IConfiguracaoRepository configuracaoRepository)
    {
        _veiculoRepository = veiculoRepository;
        _locacaoRepository = locacaoRepository;
        _entregadorRepository = entregadorRepository;
        _planoRepository = planoRepository;
        _configuracaoRepository = configuracaoRepository;
    }

    public async Task<IResultGeneric> Service(CriarLocacaoCommand action, CancellationToken ct)
    {
        action.Validate();
        if (action.Invalid) 
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosInconsistentes, action.Notifications);

        var objVeiculo = await _veiculoRepository.GetByIdEntityAsync(action.LocacaoInputDto.VeiculoId, ct);
        if (objVeiculo is null)
            return await Result.ResultAsync(false, "Veiculo inexistente, Verifique!");

        var objPlano = await _planoRepository.GetByIdEntityAsync(action.LocacaoInputDto.PlanoId, ct);
        if (objPlano is null)
            return await Result.ResultAsync(false, "Plano inexistente, Verifique!");
        
        var objEntregador = await _entregadorRepository.GetByIdEntityAsync(action.LocacaoInputDto.EntregadorId, ct);
        if (objEntregador is null)
            return await Result.ResultAsync(false, "Entregador Inexistente, Verifique!");
        
        if (objEntregador.TipoCnh != (int)TipoCnhEnum.A)
            return await Result.ResultAsync(false, "É permitido fazer locação somente entregadores com a categoria (A), Verifique!");
       
        var obj = action.LocacaoInputDto.ConvertToObject();
        obj.QuantidadeDeDiasDoPlano = objPlano.QuantidadeDias;
        obj.DataInicio = obj.DataInicio.AddDays(1);

        await _locacaoRepository.CreateAsync(obj, ct);
        return await Result.ResultAsync(true, GlobalMessageConstants.RegistroInseridoComSucesso, obj.ConvertToObjectOutPut());
    }

    public async Task<IResultGeneric> Service(InformarDataDevolucaoLocacaoCommand action, CancellationToken ct)
    {
        action.Validate();
        if (action.Invalid) 
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosInconsistentes, action.Notifications);

        var objLocacao = await _locacaoRepository.GetByIdEntityAsync(action.InformarDataDevolucaoInputDto.LocacaoId, ct);
        if (objLocacao is null)
            return await Result.ResultAsync(false, "Locação inexistente, Verifique!");

        objLocacao.DataTerminio = action.InformarDataDevolucaoInputDto.DataDevolucao;

        await _locacaoRepository.UpdateAsync(objLocacao, ct);
        return await Result.ResultAsync(true, "Data De Devolução, informada com sucesso!");
    }

    public async Task<IResultGeneric> Service(ObterDetalhesValoresLocacaoCommand action, CancellationToken ct)
    {
        action.Validate();
        if (action.Invalid) 
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosInconsistentes, action.Notifications);

        var objLocacao = await _locacaoRepository.GetByIdEntityAsync(action.LocacaoId, ct);
        if (objLocacao is null)
            return await Result.ResultAsync(false, "Locação inexistente, Verifique!");

        var objValoresLocacaoDiarias = await new TotalizadorDiariaBusiness(_configuracaoRepository, _planoRepository, _locacaoRepository, ct).RetornarValoresLocacaoDiarias(action.LocacaoId);

        return await Result.ResultAsync(true, Empty, objValoresLocacaoDiarias);
    }

    public async Task<IResultGeneric> Service(GetAllPaginationCommand action, CancellationToken ct)
    {
        action.Validate();
        if (action.Invalid) 
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosInconsistentes, action.Notifications);

        var objPaginado = await _locacaoRepository.GetAllPaginationAsync(action.PaginationParameters, ct); 
        return await Result.ResultAsync(true, Empty, objPaginado);
    }
}