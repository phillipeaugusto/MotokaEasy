using System;
using System.Threading;
using System.Threading.Tasks;
using Flunt.Notifications;
using MotokaEasy.Core.Commands;
using MotokaEasy.Core.Infrastructure.Cache.Contracts;
using MotokaEasy.Core.Messages.Shared;
using MotokaEasy.Core.Services.Contracts;
using MotokaEasy.Core.Shared;
using MotokaEasy.Core.Shared.Contracts;
using MotokaEasy.Core.Shared.Result;
using MotokaEasy.Domain.Dto.OutPutDto;
using MotokaEasy.Domain.Repositories;
using static System.String;

namespace MotokaEasy.Application.Services;

public class PlanoService:
    Notifiable,
    IService<GetAllPaginationCommand>,
    IService<GetByIdCommand>
{
    private readonly IPlanoRepository _planoRepository;
    private readonly ICache _cache;

    public PlanoService(IPlanoRepository planoRepository, ICache cache)
    {
        _planoRepository = planoRepository;
        _cache = cache;
    }

    public async Task<IResultGeneric> Service(GetAllPaginationCommand action, CancellationToken ct)
    {
        action.Validate();
        if (action.Invalid) 
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosInconsistentes, action.Notifications);

        var objPaginado = await _planoRepository.GetAllPaginationAsync(action.PaginationParameters, ct); 
        return await Result.ResultAsync(true, Empty, objPaginado);
    }

    public async Task<IResultGeneric> Service(GetByIdCommand action, CancellationToken ct)
    {
        action.Validate();
        if (action.Invalid) 
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosInconsistentes, action.Notifications);
        
        var obj = _cache.Exists(action.Id.ToString()) ? ObjectByJson.ReturnObject<PlanoOutPutDto>(_cache.GetValue(action.Id.ToString())) : await _planoRepository.GetByIdAsync(action.Id, ct);
        if (obj is null)
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosInexistentes);
        
        _cache.Save(action.Id.ToString(), obj.ConvertToJson(), TimeSpan.FromDays(1));
        return await Result.ResultAsync(true, Empty, obj);
    }
}