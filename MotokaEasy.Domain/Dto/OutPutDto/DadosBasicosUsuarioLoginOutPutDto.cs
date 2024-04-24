using System;
using MotokaEasy.Core.Infrastructure.Token;

namespace MotokaEasy.Domain.Dto.OutPutDto;

public class DadosBasicosUsuarioLoginOutPutDto
{
    public DadosBasicosUsuarioLoginOutPutDto() { }

    public DadosBasicosUsuarioLoginOutPutDto(string email, string nome, Guid usuarioId, TokenData tokenData)
    {
        Email = email;
        Nome = nome;
        TokenData = tokenData;
        UsuarioId = usuarioId;
    }

    public string Email { get; set; }
    public string Nome { get; set; }
    public Guid UsuarioId { get; set; }
    public TokenData TokenData { get; set; }
}