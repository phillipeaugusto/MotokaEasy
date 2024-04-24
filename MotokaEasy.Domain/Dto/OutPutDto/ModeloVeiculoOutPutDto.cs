using MotokaEasy.Domain.Entities;
using MotokaEasy.Core.Domain.Dto;

namespace MotokaEasy.Domain.Dto.OutPutDto;

public class ModeloVeiculoOutPutDto: Dto<ModeloVeiculoOutPutDto, ModeloVeiculo>
{
    public ModeloVeiculoOutPutDto() { }

    public string Descricao { get; set; }
}