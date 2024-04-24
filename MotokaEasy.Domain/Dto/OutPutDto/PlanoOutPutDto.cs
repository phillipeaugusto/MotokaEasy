using MotokaEasy.Domain.Entities;
using MotokaEasy.Core.Domain.Dto;

namespace MotokaEasy.Domain.Dto.OutPutDto;

public class PlanoOutPutDto: Dto<PlanoOutPutDto, Plano>
{
    public PlanoOutPutDto() { }

    public string Descricao { get; set; }
    public int QuantidadeDias { get; set; }
    public double Valor { get; set; }
}