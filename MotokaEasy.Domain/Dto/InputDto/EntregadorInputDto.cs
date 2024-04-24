using System;
using System.ComponentModel.DataAnnotations;
using MotokaEasy.Core.Domain.Dto;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Domain.Dto.InputDto;

public class EntregadorInputDto: DtoBase<EntregadorInputDto, Entregador>
{
    public EntregadorInputDto() { }

    [Required]
    public string Nome { get; set; }
    [Required]
    public string CnpjCpf { get; set; }
    [Required]
    public DateTime DataNascimento { get; set; }
    [Required]
    public string NumeroCnh { get; set; }
    [Required]
    public int TipoCnh { get; set; }
}