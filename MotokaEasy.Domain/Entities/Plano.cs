using System.Collections.Generic;
using MotokaEasy.Core.Domain.Entities;
using MotokaEasy.Domain.Dto.InputDto;
using MotokaEasy.Domain.Dto.OutPutDto;

namespace MotokaEasy.Domain.Entities;

public class Plano: Entity<Plano, PlanoInputDto, PlanoOutPutDto>
{
    public Plano() { }
    public string Descricao { get; set; }
    public int QuantidadeDias { get; set; }
    public double Valor { get; set; }
    public IEnumerable<Locacao> Locacao { get; set; }
}