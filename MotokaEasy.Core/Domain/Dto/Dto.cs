using System;

namespace MotokaEasy.Core.Domain.Dto;

public class Dto<TEntity, TDtoOutPutModel>: DtoBase<TEntity, TDtoOutPutModel>
{
    public Guid Id { get; set;}
    public string Status { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataAlteracao { get; set; }
}