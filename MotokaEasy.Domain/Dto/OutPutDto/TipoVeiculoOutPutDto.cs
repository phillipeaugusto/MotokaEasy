using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Domain.Dto.OutPutDto;
using MotokaEasy.Core.Domain.Dto;

public class TipoVeiculoOutPutDto: Dto<TipoVeiculoOutPutDto, TipoVeiculo>
{
    public string Descricao { get; set; }
}