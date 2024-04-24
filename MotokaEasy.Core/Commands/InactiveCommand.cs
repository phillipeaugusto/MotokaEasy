using System;
using Flunt.Notifications;
using Flunt.Validations;
using MotokaEasy.Core.Commands.Dto.InputDto;
using MotokaEasy.Core.Domain.Contracts;
using MotokaEasy.Core.Messages.Shared;

namespace MotokaEasy.Core.Commands;

public class InactiveCommand: Notifiable, IValidator
{
    public InactiveCommand(InactiveCommandIdInputDto inactiveCommandIdInputDto)
    {
        InactiveCommandIdInputDto = inactiveCommandIdInputDto;
    }
    public InactiveCommandIdInputDto InactiveCommandIdInputDto { get; set; }
    public void Validate()
    {
        AddNotifications(
            new Contract()
                .Requires()
                .IsFalse(InactiveCommandIdInputDto.Id == Guid.Empty, "Id", GlobalMessageConstants.CampoInvalidoOuInexistente)
                .IsNotNull(InactiveCommandIdInputDto.Active, "Active", GlobalMessageConstants.CampoInvalidoOuInexistente)
       );
    }
}