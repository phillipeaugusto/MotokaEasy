using System;
using MotokaEasy.Core.Domain.Entities;
using MotokaEasy.Domain.Dto.InputDto;
using MotokaEasy.Domain.Dto.OutPutDto;

namespace MotokaEasy.Domain.Entities;

public class Locacao: Entity<Locacao, LocacaoInputDto, LocacaoOutPutDto>
{
    public Locacao() { }

    public Guid EntregadorId { get; set; }
    public Guid PlanoId { get; set; }
    public int QuantidadeDeDiasDoPlano { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataTerminio { get; set; }
    public DateTime DataPrevisaoTerminio { get; set; }
    public Guid VeiculoId { get; set; }
    public Veiculo Veiculo { get; set; }
    public Entregador Entregador { get; set; }
    public Plano Plano { get; set; }
}