using System;
using System.ComponentModel.DataAnnotations;
using MotokaEasy.Core.Domain.Dto;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Domain.Dto.InputDto;

public class VeiculoInputDto: DtoBase<VeiculoInputDto, Veiculo>
{
    public VeiculoInputDto() { }

    [Required]
    public int Ano { get; set; }
    [Required]
    public string Placa { get; set; }
    [Required]
    public Guid ModeloVeiculoId { get; set; }
    [Required]
    public Guid TipoVeiculoId { get; set; }
}