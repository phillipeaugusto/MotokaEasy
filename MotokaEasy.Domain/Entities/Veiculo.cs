using System;
using System.Collections.Generic;
using MotokaEasy.Core.Domain.Entities;
using MotokaEasy.Domain.Dto.InputDto;
using MotokaEasy.Domain.Dto.OutPutDto;

namespace MotokaEasy.Domain.Entities;

public class Veiculo: Entity<Veiculo, VeiculoInputDto, VeiculoOutPutDto>
{
    public Veiculo() { }

    public int Ano { get; set; }
    public Guid ModeloVeiculoId { get; set; }
    public string Placa { get; set; }
    public Guid TipoVeiculoId { get; set; }
    public ModeloVeiculo ModeloVeiculo { get; set; }
    public TipoVeiculo TipoVeiculo { get; set; }
    public IEnumerable<Locacao> Locacao { get; set; }
}