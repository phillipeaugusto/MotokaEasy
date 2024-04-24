using System;
using MotokaEasy.Domain.Entities;
using MotokaEasy.Core.Domain.Dto;

namespace MotokaEasy.Domain.Dto.OutPutDto;

public class VeiculoOutPutDto: Dto<VeiculoOutPutDto, Veiculo>
{
    public VeiculoOutPutDto() { }

    public int Ano { get; set; }
    public string Placa { get; set; }
    public Guid ModeloVeiculoId { get; set; }
    public Guid TipoVeiculoId { get; set; }
}