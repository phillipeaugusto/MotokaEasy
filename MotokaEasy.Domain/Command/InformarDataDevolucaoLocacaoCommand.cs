using System;
using Flunt.Notifications;
using Flunt.Validations;
using MotokaEasy.Core.Domain.Contracts;
using MotokaEasy.Core.Messages.Shared;
using MotokaEasy.Domain.Dto.InputDto;

namespace MotokaEasy.Domain.Command;

public class InformarDataDevolucaoLocacaoCommand: Notifiable, IValidator
{
    public InformarDataDevolucaoLocacaoCommand(InformarDataDevolucaoInputDto informarDataDevolucaoInputDto)
    {
        InformarDataDevolucaoInputDto = informarDataDevolucaoInputDto;
    }

    public InformarDataDevolucaoInputDto InformarDataDevolucaoInputDto { get; set; }
    public void Validate()
    {
        AddNotifications(
            new Contract()
                .Requires()
                .IsFalse(InformarDataDevolucaoInputDto.DataDevolucao.Date < DateTime.Now.Date, "DataDevolucao", "A data de devolução deve ser maior ou igual a data atual, verifique!")
                .IsFalse(InformarDataDevolucaoInputDto.LocacaoId == Guid.Empty, "LocacaoId", GlobalMessageConstants.CampoInvalidoOuInexistente)
        );
    }
}