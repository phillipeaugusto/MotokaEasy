using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MotokaEasy.Core.Commands.Dto.InputDto;
[ExcludeFromCodeCoverage]
public class ChangeStatusInputDto
{
    public ChangeStatusInputDto(Guid id, bool active)
    {
        Id = id;
        Active = active;
    }

    [Required]
    public Guid Id { get; set; }
    [Required]
    public bool Active { get; set; }
}