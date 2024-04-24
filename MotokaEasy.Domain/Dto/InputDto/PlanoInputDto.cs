using System.ComponentModel.DataAnnotations;
using MotokaEasy.Core.Domain.Dto;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Domain.Dto.InputDto;

public class PlanoInputDto: DtoBase<PlanoInputDto, Plano>
{
    public PlanoInputDto() { }

    public PlanoInputDto(string descricao, int quantidadeDias, float valor)
    {
        Descricao = descricao;
        QuantidadeDias = quantidadeDias;
        Valor = valor;
    }

    [Required]
    public string Descricao { get; set; }
    [Required]
    public int QuantidadeDias { get; set; }
    [Required]
    public double Valor { get; set; }
}