using System.Threading;
using System.Threading.Tasks;
using Flunt.Notifications;
using MotokaEasy.Core.Commands;
using MotokaEasy.Core.Messages.Shared;
using MotokaEasy.Core.Services.Contracts;
using MotokaEasy.Core.Shared.Contracts;
using MotokaEasy.Core.Shared.Result;
using MotokaEasy.Domain.Repositories;
using static System.String;

namespace MotokaEasy.Application.Services;

public class ModeloVeiculoService:
    Notifiable,
    IService<GetAllPaginationCommand>,
    IService<GetByIdCommand>
{
    private readonly IModeloVeiculoRepository _modeloVeiculoRepository;

    public ModeloVeiculoService(IModeloVeiculoRepository modeloVeiculoRepository)
    {
        _modeloVeiculoRepository = modeloVeiculoRepository;
    }

    public async Task<IResultGeneric> Service(GetAllPaginationCommand action, CancellationToken ct)
    {
        action.Validate();
        if (action.Invalid) 
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosInconsistentes, action.Notifications);

        var objPaginado = await _modeloVeiculoRepository.GetAllPaginationAsync(action.PaginationParameters, ct); 
        return await Result.ResultAsync(true, Empty, objPaginado);
    }

    public async Task<IResultGeneric> Service(GetByIdCommand action, CancellationToken ct)
    {
        action.Validate();
        if (action.Invalid) 
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosInconsistentes, action.Notifications);

        var obj = await _modeloVeiculoRepository.GetByIdAsync(action.Id, ct); 
        if (obj is null)
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosInexistentes);
        
        return await Result.ResultAsync(true, Empty, obj);
    }
}