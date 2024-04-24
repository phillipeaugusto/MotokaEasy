using System;
using Flunt.Notifications;
using Flunt.Validations;
using MotokaEasy.Core.Domain.Contracts;
using MotokaEasy.Core.Messages.Shared;

namespace MotokaEasy.Domain.Command;

public class RemoverVeiculoCommand: Notifiable, IValidator
{
    public RemoverVeiculoCommand(Guid veiculoId)
    {
        VeiculoId = veiculoId;
    }

    public Guid VeiculoId { get; set; }
    
    public void Validate()
    {
        AddNotifications(
            new Contract()
                .Requires()
                .IsFalse(VeiculoId == Guid.Empty, "VeiculoId", GlobalMessageConstants.CampoInvalidoOuInexistente)
        );
    }
}