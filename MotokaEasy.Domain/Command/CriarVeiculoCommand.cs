using System;
using Flunt.Notifications;
using Flunt.Validations;
using MotokaEasy.Core.Domain.Contracts;
using MotokaEasy.Core.Messages.Shared;
using MotokaEasy.Domain.Dto.InputDto;

namespace MotokaEasy.Domain.Command;

public class CriarVeiculoCommand: Notifiable, IValidator
{
    public CriarVeiculoCommand(VeiculoInputDto veiculoInputDto)
    {
        VeiculoInputDto = veiculoInputDto;
    }

    public VeiculoInputDto VeiculoInputDto { get; set; }
    
    public void Validate()
    {
        AddNotifications(
            new Contract()
                .Requires()
                .IsFalse(VeiculoInputDto.TipoVeiculoId == Guid.Empty, "TipoVeiculoId", GlobalMessageConstants.CampoInvalidoOuInexistente)
                .IsFalse(VeiculoInputDto.ModeloVeiculoId == Guid.Empty, "ModeloVeiculoId", GlobalMessageConstants.CampoInvalidoOuInexistente)
                .IsFalse(VeiculoInputDto.Ano > DateTime.Now.Year, "Ano", "Ano do veiculo deve ser menor ou igual o ano atual.")
                .IsFalse(VeiculoInputDto.Ano.ToString().Length < 4 , "Ano", "Ano do veiculo deve conter 4 digitos ex: '1995'.")
                .IsFalse(VeiculoInputDto.Placa.Length is < 8 or > 8, "Placa", "Placa inválida, a mesma deve ter o formato ex: 'AAA-12A2'.")
        );
    }
}