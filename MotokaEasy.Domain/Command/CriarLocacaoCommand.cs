using System;
using Flunt.Notifications;
using Flunt.Validations;
using MotokaEasy.Core.Domain.Contracts;
using MotokaEasy.Core.Messages.Shared;
using MotokaEasy.Domain.Dto.InputDto;

namespace MotokaEasy.Domain.Command;

public class CriarLocacaoCommand: Notifiable, IValidator
{
    public CriarLocacaoCommand(LocacaoInputDto locacaoInputDto)
    {
        LocacaoInputDto = locacaoInputDto;
    }

    public LocacaoInputDto LocacaoInputDto { get; set; }

    public void Validate()
    {
        AddNotifications(
            new Contract()
                .Requires()
                .IsFalse(LocacaoInputDto.EntregadorId == Guid.Empty, "EntregadorId", GlobalMessageConstants.CampoInvalidoOuInexistente)
                .IsFalse(LocacaoInputDto.PlanoId == Guid.Empty, "PlanoId", GlobalMessageConstants.CampoInvalidoOuInexistente)
                .IsFalse(LocacaoInputDto.DataInicio.Date < DateTime.Now.Date, "DataInicio", "Data Inicio, deve ser maior ou igual a data de hoje, verifique!")
                .IsFalse(LocacaoInputDto.DataTerminio.Date < LocacaoInputDto.DataInicio.Date, "DataTerminio", "Data Terminio, não pode ser menor que a data de inicio, verifique!")
                .IsFalse(LocacaoInputDto.DataPrevisaoTerminio.Date < LocacaoInputDto.DataInicio.Date, "DataPrevisaoTerminio", "Data PrevisaoTerminio, deve ser maior ou igual a data de inicio, verifique!")
                .IsFalse(LocacaoInputDto.VeiculoId == Guid.Empty, "VeiculoId", GlobalMessageConstants.CampoInvalidoOuInexistente)
        );
    }
}