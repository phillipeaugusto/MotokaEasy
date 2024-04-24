using System;

namespace MotokaEasy.Core.Domain.Entities;

public class Entity<TEntity, TDtoOutPutModel, TOutPut> : EntityBase<TEntity, TDtoOutPutModel, TOutPut>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Status { get; set; } = "A";
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public DateTime DataAlteracao { get; set;} = DateTime.Now;

    public void SetId(Guid id)
    {
        Id = id;
    }

    public Guid GetId()
    {
        return Id;
    }
}