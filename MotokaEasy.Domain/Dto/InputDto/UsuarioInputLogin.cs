using System.ComponentModel.DataAnnotations;
using MotokaEasy.Core.Domain.Dto;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Domain.Dto.InputDto;

public class UsuarioInputLogin: DtoBase<UsuarioInputLogin, Usuario>
{
    public UsuarioInputLogin() { }

    public UsuarioInputLogin(string email, string senha)
    {
        Email = email;
        Senha = senha;
    }

    [Required]
    public string Email { get; set; }
    [Required]
    public string Senha { get; set; }
}