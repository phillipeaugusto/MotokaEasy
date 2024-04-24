using Flunt.Notifications;
using Flunt.Validations;
using MotokaEasy.Core.Domain.Contracts;
using MotokaEasy.Core.Messages.Shared;
using MotokaEasy.Domain.Dto.InputDto;

namespace MotokaEasy.Domain.Command;

public class CriarUsuarioCommand: Notifiable, IValidator
{
    public CriarUsuarioCommand(UsuarioInputDto usuarioInputDto)
    {
        UsuarioInputDto = usuarioInputDto;
    }

    public UsuarioInputDto UsuarioInputDto { get; set; }

    public void Validate()
    {
        
        AddNotifications(
            new Contract()
                .Requires()
                .IsFalse(UsuarioInputDto.Nome == string.Empty || UsuarioInputDto.Nome.Length > 60, "Nome", GlobalMessageConstants.CampoInvalidoOuInexistente)
                .IsFalse(UsuarioInputDto.Numero == string.Empty || UsuarioInputDto.Numero.Length < 10 , "Numero", GlobalMessageConstants.CampoInvalidoOuInexistente)
                .IsEmail(UsuarioInputDto.Email, "Email", "Email inválido!")
                .IsFalse(UsuarioInputDto.Senha == string.Empty || UsuarioInputDto.Senha.Length > 8 || UsuarioInputDto.Senha.Length < 8, "Senha", GlobalMessageConstants.CampoInvalidoOuInexistente)
        );
    }
}