using System.ComponentModel.DataAnnotations;
using MotokaEasy.Core.Domain.Dto;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Domain.Dto.InputDto;

public class TipoVeiculoInputDto: DtoBase<TipoVeiculoInputDto, TipoVeiculo>
{
    public TipoVeiculoInputDto() { }

    public TipoVeiculoInputDto(string descricao)
    {
        Descricao = descricao;
    }

    [Required]
    public string Descricao { get; set; }
}