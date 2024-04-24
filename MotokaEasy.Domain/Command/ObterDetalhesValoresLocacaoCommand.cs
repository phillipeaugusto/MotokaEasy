using System;
using Flunt.Notifications;
using Flunt.Validations;
using MotokaEasy.Core.Domain.Contracts;
using MotokaEasy.Core.Messages.Shared;

namespace MotokaEasy.Domain.Command;

public class ObterDetalhesValoresLocacaoCommand: Notifiable, IValidator
{
    public ObterDetalhesValoresLocacaoCommand(Guid locacaoId)
    {
        LocacaoId = locacaoId;
    }

    public Guid LocacaoId { get; set; }
    
    public void Validate()
    {
        AddNotifications(
            new Contract()
                .Requires()
                .IsFalse(LocacaoId == Guid.Empty, "LocacaoId", GlobalMessageConstants.CampoInvalidoOuInexistente)
        );
    }
}