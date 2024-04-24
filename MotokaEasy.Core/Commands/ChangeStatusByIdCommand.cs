using System;
using Flunt.Notifications;
using Flunt.Validations;
using MotokaEasy.Core.Commands.Dto.InputDto;
using MotokaEasy.Core.Domain.Contracts;
using MotokaEasy.Core.Messages.Shared;

namespace MotokaEasy.Core.Commands;

public class ChangeStatusByIdCommand: Notifiable, IValidator
{
    public ChangeStatusByIdCommand(ChangeStatusInputDto changeStatusInputDto)
    {
        ChangeStatusInputDto = changeStatusInputDto;
    }

    public ChangeStatusInputDto ChangeStatusInputDto { get; set; }
    public void Validate()
    {
        AddNotifications(
            new Contract()
                .Requires()
                .IsFalse(ChangeStatusInputDto.Id == Guid.Empty, "Id", GlobalMessageConstants.CampoInvalidoOuInexistente)
                .IsNotNull(ChangeStatusInputDto.Active, "Active", GlobalMessageConstants.CampoInvalidoOuInexistente)
        );
    }
}