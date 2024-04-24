using System;
using Flunt.Notifications;
using Flunt.Validations;
using MotokaEasy.Core.Domain.Contracts;
using MotokaEasy.Core.Messages.Shared;

namespace MotokaEasy.Core.Commands;

public class GetByIdCommand: Notifiable, IValidator
{
    public GetByIdCommand(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
    public void Validate()
    {
        AddNotifications(
            new Contract()
                .Requires()
                .IsFalse(Id == Guid.Empty, "Id", GlobalMessageConstants.CampoInvalidoOuInexistente)
        );
    }
}