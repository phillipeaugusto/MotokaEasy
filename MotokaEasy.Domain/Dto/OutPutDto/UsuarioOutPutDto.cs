using MotokaEasy.Domain.Entities;
using MotokaEasy.Core.Domain.Dto;

namespace MotokaEasy.Domain.Dto.OutPutDto;

public class UsuarioOutPutDto: Dto<UsuarioOutPutDto, Usuario>
{
    public UsuarioOutPutDto() { }

    public string Nome { get; set; }
    public string Email { get; set; }
    public string Numero { get; set; }
}