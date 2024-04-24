using System.ComponentModel.DataAnnotations;
using MotokaEasy.Core.Domain.Dto;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Domain.Dto.InputDto;

public class UsuarioInputDto: DtoBase<UsuarioInputDto, Usuario>
{
    public UsuarioInputDto() { }

    public UsuarioInputDto(string nome, string email, string senha, string numero)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
        Numero = numero;
    }

    [Required]
    public string Nome { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Senha { get; set; }
    [Required]
    public string Numero { get; set; }
}