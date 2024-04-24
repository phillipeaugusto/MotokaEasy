using System.Collections.Generic;
using MotokaEasy.Core.Domain.Entities;
using MotokaEasy.Domain.Dto.InputDto;
using MotokaEasy.Domain.Dto.OutPutDto;

namespace MotokaEasy.Domain.Entities;

public class ModeloVeiculo: Entity<ModeloVeiculo, ModeloVeiculoInputDto, ModeloVeiculoOutPutDto>
{
    public ModeloVeiculo() { }
    public string Descricao { get; set; }
    public IEnumerable<Veiculo> Veiculo { get; set; }
}