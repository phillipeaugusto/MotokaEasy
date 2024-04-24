using System;
using MotokaEasy.Domain.Entities;
using MotokaEasy.Core.Domain.Dto;

namespace MotokaEasy.Domain.Dto.OutPutDto;

public class EntregadorOutPutDto: Dto<EntregadorOutPutDto, Entregador>
{
    public EntregadorOutPutDto() { }

    public string Nome { get; set; }
    public string CnpjCpf { get; set; } 
    public DateTime DataNascimento { get; set; }
    public string NumeroCnh { get; set; }
    public int TipoCnh { get; set; }
    public string ImagemCnh { get; set; }
}