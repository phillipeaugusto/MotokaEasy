using System;
using System.ComponentModel.DataAnnotations;
using MotokaEasy.Core.Domain.Dto;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Domain.Dto.InputDto;

public class LocacaoInputDto: DtoBase<LocacaoInputDto, Locacao>
{
    public LocacaoInputDto() { }

    [Required]
    public Guid EntregadorId { get; set; }
    [Required]
    public Guid PlanoId { get; set; }
    [Required]
    public DateTime DataInicio { get; set; }
    [Required]
    public DateTime DataTerminio { get; set; }
    [Required]
    public DateTime DataPrevisaoTerminio { get; set; }
    [Required]
    public Guid VeiculoId { get; set; }
}