using System;
using Flunt.Notifications;
using Flunt.Validations;
using MotokaEasy.Core.Domain.Contracts;
using MotokaEasy.Domain.Dto.InputDto;

namespace MotokaEasy.Domain.Command;

public class AlterarPlacaVeiculoCommand: Notifiable, IValidator
{
    public AlterarPlacaVeiculoCommand(Guid veiculoId, AlterarPlacaInputDto alterarPlacaInputDto)
    {
        VeiculoId = veiculoId;
        AlterarPlacaInputDto = alterarPlacaInputDto;
    }

    public Guid VeiculoId { get; set; }
    public AlterarPlacaInputDto AlterarPlacaInputDto { get; set; }
    
    public void Validate()
    {
        AddNotifications(
            new Contract()
                .Requires()
                .IsFalse(VeiculoId == Guid.Empty, "VeiculoId", "Código inválido, verifique.")
                .IsFalse(AlterarPlacaInputDto.Placa.Length < 7, "Placa", "Placa inválida, a mesma deve ter o formato ex: 'AAA-12A2'.")
        );
    }
}
