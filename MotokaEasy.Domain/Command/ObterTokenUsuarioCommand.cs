using Flunt.Notifications;
using Flunt.Validations;
using MotokaEasy.Core.Domain.Contracts;
using MotokaEasy.Core.Messages.Shared;
using MotokaEasy.Domain.Dto.InputDto;
using static System.String;

namespace MotokaEasy.Domain.Command;

public class ObterTokenUsuarioCommand: Notifiable, IValidator
{
    public ObterTokenUsuarioCommand() { }

    public ObterTokenUsuarioCommand(UsuarioInputLogin usuarioInputLogin)
    {
        UsuarioInputLogin = usuarioInputLogin;
    }
    
    public UsuarioInputLogin UsuarioInputLogin { get; set; }
    
    public void Validate()
    {
        AddNotifications(
            new Contract()
                .Requires()
                .IsFalse(UsuarioInputLogin.Email == Empty, "Usuario", GlobalMessageConstants.CampoInvalidoOuInexistente)
                .IsFalse(UsuarioInputLogin.Senha == Empty, "Senha", GlobalMessageConstants.CampoInvalidoOuInexistente)
                .IsFalse(UsuarioInputLogin.Senha.Length > 8 , "Senha", "A senha deve conter no maximo 8 caracteres, verifique!")
        );
    }
}