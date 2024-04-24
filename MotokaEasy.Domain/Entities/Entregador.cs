using System;
using System.Collections.Generic;
using MotokaEasy.Core.Domain.Entities;
using MotokaEasy.Domain.Dto.InputDto;
using MotokaEasy.Domain.Dto.OutPutDto;

namespace MotokaEasy.Domain.Entities;

public class Entregador: Entity<Entregador, EntregadorInputDto, EntregadorOutPutDto>
{
    public Entregador() { }

    public string Nome { get; set; }
    public string CnpjCpf { get; set; } 
    public DateTime DataNascimento { get; set; }
    public string NumeroCnh { get; set; }
    public int TipoCnh { get; set; }
    public string ImagemCnh { get; set; }
    public IEnumerable<Locacao> Locacao { get; set; }
}