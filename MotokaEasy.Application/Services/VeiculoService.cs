using System.Threading;
using System.Threading.Tasks;
using Flunt.Notifications;
using MotokaEasy.Core.Commands;
using MotokaEasy.Core.Infrastructure.MessageBroker.Contracts;
using MotokaEasy.Core.Messages.Shared;
using MotokaEasy.Core.Services.Contracts;
using MotokaEasy.Core.Shared.Contracts;
using MotokaEasy.Core.Shared.Result;
using MotokaEasy.Domain.Command;
using MotokaEasy.Domain.Events;
using MotokaEasy.Domain.Repositories;
using MotokaEasy.Domain.Shared;
using static System.String;

namespace MotokaEasy.Application.Services;

public class VeiculoService:
    Notifiable,
    IService<CriarVeiculoCommand>,
    IService<AlterarPlacaVeiculoCommand>,
    IService<RemoverVeiculoCommand>,
    IService<GetAllPaginationCommand>,
    IService<BuscarDadosDoVeiculoViaPlacaCommand>
{
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly ILocacaoRepository _locacaoRepository;
    private readonly ITipoVeiculoRepository _tipoVeiculoRepository;
    private readonly IModeloVeiculoRepository _modeloVeiculoRepository;
    private IMessageBroker _messageBroker;

    public VeiculoService(IVeiculoRepository veiculoRepository, ILocacaoRepository locacaoRepository, IModeloVeiculoRepository modeloVeiculoRepository, ITipoVeiculoRepository tipoVeiculoRepository, IMessageBroker messageBroker)
    {
        _veiculoRepository = veiculoRepository;
        _locacaoRepository = locacaoRepository;
        _modeloVeiculoRepository = modeloVeiculoRepository;
        _tipoVeiculoRepository = tipoVeiculoRepository;
        _messageBroker = messageBroker;
    }

    public async Task<IResultGeneric> Service(CriarVeiculoCommand action, CancellationToken ct)
    {
        action.Validate();
        if (action.Invalid) 
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosInconsistentes, action.Notifications);

        var objValicaoTipoVeiculo = await _tipoVeiculoRepository.GetByIdEntityAsync(action.VeiculoInputDto.TipoVeiculoId, ct);
        if (objValicaoTipoVeiculo is null)
            return await Result.ResultAsync(false, "Tipo De Veiculo Inexistente, Verifique!");

        var objValicaoModelo = await _modeloVeiculoRepository.GetByIdEntityAsync(action.VeiculoInputDto.ModeloVeiculoId, ct);
        if (objValicaoModelo is null)
            return await Result.ResultAsync(false, "Modelo De Veiculo Inexistente, Verifique!");

        var placaExiste = await _veiculoRepository.ValidarSePlacaJaExisteAsync(action.VeiculoInputDto.Placa, ct);
        if (placaExiste)
            return await Result.ResultAsync(false, "Placa já vinculada á um veiculo, verifique!");

        var obj = action.VeiculoInputDto.ConvertToObject();
        
        var objTemp = await _veiculoRepository.ExistsAsync(obj, ct);
        if (objTemp is not null)
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosJaExistentes);

        await _veiculoRepository.CreateAsync(obj, ct);
        return await Result.ResultAsync(true, GlobalMessageConstants.RegistroInseridoComSucesso, obj.ConvertToObjectOutPut());
    }

    public async Task<IResultGeneric> Service(AlterarPlacaVeiculoCommand action, CancellationToken ct)
    {
        action.Validate();
        if (action.Invalid) 
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosInconsistentes, action.Notifications);

        var placaExiste = await _veiculoRepository.ValidarSePlacaJaExisteAsync(action.AlterarPlacaInputDto.Placa, ct);
        if (placaExiste) 
            return await Result.ResultAsync(false, "A nova placa informada, já esta vinculada a outro veículo, verifique!");

        var objTemp = await _veiculoRepository.GetByIdEntityAsync(action.VeiculoId, ct);
        if (objTemp is null)
            return await Result.ResultAsync(false, "Veículo não encontrado!, verifique!");

        _messageBroker.PublishQueue(QueueConstants.QueueAtualizarPlacaVeiculo, new AtualizarNumeroPlacaVeiculoEvent(action.VeiculoId, action.AlterarPlacaInputDto.Placa));
        return await Result.ResultAsync(true, GlobalMessageConstants.RegistroAlteradoComSucesso);
    }

    public async Task<IResultGeneric> Service(RemoverVeiculoCommand action, CancellationToken ct)
    {
        action.Validate();
        if (action.Invalid) 
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosInconsistentes, action.Notifications);

        var objVeiculo = await _veiculoRepository.GetByIdEntityAsync(action.VeiculoId, ct);
        if (objVeiculo is null)
            return await Result.ResultAsync(false, "Veículo não encontrado!, verifique!");
        
        var objValidacaoVinculo = await _locacaoRepository.ValidarSeVeiculoPossuiRegistroDeLocacaoAsync(action.VeiculoId, ct);
        if (objValidacaoVinculo)
            return await Result.ResultAsync(false, "Veículo já possui, vinculo com alguma locação e não pode ser excluido!");

        await _veiculoRepository.DeleteAsync(objVeiculo, ct);
        return await Result.ResultAsync(true, GlobalMessageConstants.RegistroExcluidoComSucesso);
    }

    public async Task<IResultGeneric> Service(GetAllPaginationCommand action, CancellationToken ct)
    {
        action.Validate();
        if (action.Invalid) 
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosInconsistentes, action.Notifications);

        var objPaginado = await _veiculoRepository.GetAllPaginationAsync(action.PaginationParameters, ct); 
        return await Result.ResultAsync(true, Empty, objPaginado);
    }

    public async Task<IResultGeneric> Service(BuscarDadosDoVeiculoViaPlacaCommand action, CancellationToken ct)
    {
        action.Validate();
        if (action.Invalid) 
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosInconsistentes, action.Notifications);
        
        var objVeiculo = await _veiculoRepository.BuscarVeiculoPelaPlacaAsync(action.Placa, ct);
        if (objVeiculo is null)
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosInexistentes);

        return await Result.ResultAsync(true, Empty, objVeiculo);
    }
}