using System.ComponentModel.DataAnnotations;
using MotokaEasy.Core.Domain.Dto;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Domain.Dto.InputDto;

public class ModeloVeiculoInputDto: DtoBase<ModeloVeiculoInputDto, ModeloVeiculo>
{
    public ModeloVeiculoInputDto() { }

    public ModeloVeiculoInputDto(string descricao)
    {
        Descricao = descricao;
    }


    [Required]
    public string Descricao { get; set; }
}