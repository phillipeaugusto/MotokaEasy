using System.Threading;
using System.Threading.Tasks;
using Flunt.Notifications;
using MotokaEasy.Core.Infrastructure.Cloud.Contracts;
using MotokaEasy.Core.Messages.Shared;
using MotokaEasy.Core.Services.Contracts;
using MotokaEasy.Core.Shared.Contracts;
using MotokaEasy.Core.Shared.Result;
using MotokaEasy.Domain.Command;
using MotokaEasy.Domain.Repositories;

namespace MotokaEasy.Application.Services;

public class EntregadorService:
    Notifiable,
    IService<CriarEntregadorCommand>,
    IService<UploadDocumentoEntregadorCommand>
{
    private readonly IEntregadorRepository _entregadorRepository;
    private readonly ICloud _cloud;

    public EntregadorService(IEntregadorRepository entregadorRepository, ICloud cloud)
    {
        _entregadorRepository = entregadorRepository;
        _cloud = cloud;
    }

    public async Task<IResultGeneric> Service(CriarEntregadorCommand action, CancellationToken ct)
    {
        action.Validate();
        if (action.Invalid) 
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosInconsistentes, action.Notifications);

        var cnhVinculada = await _entregadorRepository.ValidarSeCnhEstaVinculadaAlgumEntregadorAsync(action.EntregadorInputDto.NumeroCnh, ct);
        if (cnhVinculada)
            return await Result.ResultAsync(false, "A CNH informada já está vinculada a um entregador, verifique!");

        var cnpjCpfVinculado = await _entregadorRepository.ValidarSeCnpjCpfEstaVinculadoAlgumEntregadorAsync(action.EntregadorInputDto.CnpjCpf, ct);
        if (cnpjCpfVinculado)
            return await Result.ResultAsync(false, "O CnpjCpf já está vinculado a um entregador, verifique!");
        
        if (action.EntregadorInputDto.TipoCnh > 3 && action.EntregadorInputDto.TipoCnh < 0)
            return await Result.ResultAsync(false, "O Tipo de CNH, informada não é valido, verifique! ");
            
        var obj = action.EntregadorInputDto.ConvertToObject();

        var objTemp = await _entregadorRepository.ExistsAsync(obj, ct);
        if (objTemp is not null)
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosJaExistentes);

        await _entregadorRepository.CreateAsync(obj, ct);
        return await Result.ResultAsync(true, GlobalMessageConstants.RegistroInseridoComSucesso, obj.ConvertToObjectOutPut());
    }

    public async Task<IResultGeneric> Service(UploadDocumentoEntregadorCommand action, CancellationToken ct)
    {
        action.Validate();
        if (action.Invalid) 
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosInconsistentes, action.Notifications);

        var objEntregador = await _entregadorRepository.GetByIdAsync(action.EntregadorId, ct);
        if (objEntregador is null)
            return await Result.ResultAsync(false, "Entregador inexistente, verifique!", action.Notifications);

        var dadosUpload = await _cloud.UploadFileAsync("motokaeasy-cnh-entregador", objEntregador.Id.ToString(), action.File.OpenReadStream());
        if (!dadosUpload.Sucesso)
            return await Result.ResultAsync(false, dadosUpload.Mensagem);

        await _entregadorRepository.AtualizarUrlCnhEntregadorAsync(action.EntregadorId, dadosUpload.UrlArquivo, ct);
        return await Result.ResultAsync(true, GlobalMessageConstants.OperacaoFinalizadaComSucesso);
    }
}