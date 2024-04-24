using System;
using Flunt.Notifications;
using Flunt.Validations;
using MotokaEasy.Core.Domain.Contracts;
using MotokaEasy.Core.Messages.Shared;

namespace MotokaEasy.Core.Commands;

public class RemoveByIdCommand : Notifiable, IValidator
{

    public RemoveByIdCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
    public void Validate()
    {
        AddNotifications(
            new Contract()
                .Requires()
                .IsNotEmpty(Id, "Id", GlobalMessageConstants.CampoInvalidoOuInexistente)
        );
    }
}