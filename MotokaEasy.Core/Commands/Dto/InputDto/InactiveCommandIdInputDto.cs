using System;
using System.ComponentModel.DataAnnotations;

namespace MotokaEasy.Core.Commands.Dto.InputDto;

public class InactiveCommandIdInputDto
{
    public InactiveCommandIdInputDto(Guid id, bool active)
    {
        Id = id;
    }
    [Required]
    public Guid Id { get; set; }
    [Required]
    public bool Active { get; set; }
}