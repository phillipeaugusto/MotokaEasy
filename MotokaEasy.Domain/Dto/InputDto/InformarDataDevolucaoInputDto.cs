using System;

namespace MotokaEasy.Domain.Dto.InputDto;

public class InformarDataDevolucaoInputDto
{
    public InformarDataDevolucaoInputDto() { }

    public Guid LocacaoId { get; set; }
    public DateTime DataDevolucao { get; set; }
}