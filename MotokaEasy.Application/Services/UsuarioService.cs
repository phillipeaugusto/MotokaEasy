using System.Threading;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.Extensions.Configuration;
using MotokaEasy.Core.Messages.Shared;
using MotokaEasy.Core.Services.Contracts;
using MotokaEasy.Core.Shared.Contracts;
using MotokaEasy.Core.Shared.Result;
using MotokaEasy.Core.Shared.Utils;
using MotokaEasy.Domain.Command;
using MotokaEasy.Domain.Dto.OutPutDto;
using MotokaEasy.Domain.Repositories;
using static System.String;

namespace MotokaEasy.Application.Services;

public class UsuarioService:
    Notifiable,
    IService<CriarUsuarioCommand>,
    IService<ObterTokenUsuarioCommand>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IConfiguration _configuration;

    public UsuarioService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
    {
        _usuarioRepository = usuarioRepository;
        _configuration = configuration;
    }

    public async Task<IResultGeneric> Service(CriarUsuarioCommand action, CancellationToken ct)
    {
        action.Validate();
        if (action.Invalid) 
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosInconsistentes, action.Notifications);

        var obj = action.UsuarioInputDto.ConvertToObject();
        obj.SetRoleUser();
        obj.CriptografarPassWord();
        
        var emailCadastrado = await _usuarioRepository.ValidarSeEmailEstaCadastradoAsync(obj.Email, ct);
        if (emailCadastrado)
            return await Result.ResultAsync(false, "Usuário já Cadastrado!.");

        await _usuarioRepository.CreateAsync(obj, ct);
        return await Result.ResultAsync(true, GlobalMessageConstants.RegistroInseridoComSucesso, obj.ConvertToObjectOutPut());
    }

    public async Task<IResultGeneric> Service(ObterTokenUsuarioCommand action, CancellationToken ct)
    {
        action.Validate();
        if (action.Invalid) 
            return await Result.ResultAsync(false, GlobalMessageConstants.DadosInconsistentes, action.Notifications);

        var objTemp = action.UsuarioInputLogin.ConvertToObject();
        objTemp.CriptografarPassWord();
        var obj = await _usuarioRepository.ExistsAsync(objTemp, ct);
        
        if (obj is null)
            return await Result.ResultAsync(false, "Usuário ou Senha, Inválido(s), Verifique!");

        var dadosToken = TokenService.ReturnTokenData(obj.Id, obj.Role, _configuration.GetSection("Token").Value);
        return await Result.ResultAsync(true, Empty, new DadosBasicosUsuarioLoginOutPutDto(obj.Email, obj.Nome, obj.Id, dadosToken));
    }
}