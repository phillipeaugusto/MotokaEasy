using System;
using MotokaEasy.Domain.Entities;
using MotokaEasy.Core.Domain.Dto;

namespace MotokaEasy.Domain.Dto.OutPutDto;

public class LocacaoOutPutDto: Dto<UsuarioOutPutDto, Locacao>
{
    public LocacaoOutPutDto() { }

    public Guid EntregadorId { get; set; }
    public Guid PlanoId { get; set; }
    public int QuantidadeDeDiasDoPlano { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataTerminio { get; set; }
    public DateTime DataPrevisaoTerminio { get; set; }
    public Guid VeiculoId { get; set; }
}