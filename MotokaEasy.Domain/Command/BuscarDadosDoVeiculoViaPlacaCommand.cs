using Flunt.Notifications;
using Flunt.Validations;
using MotokaEasy.Core.Domain.Contracts;

namespace MotokaEasy.Domain.Command;

public class BuscarDadosDoVeiculoViaPlacaCommand: Notifiable, IValidator
{
    public BuscarDadosDoVeiculoViaPlacaCommand(string placa)
    {
        Placa = placa;
    }

    public string Placa { get; set; }
    
    public void Validate()
    {
        AddNotifications(
            new Contract()
                .Requires()
                .IsFalse(Placa.Length < 7, "Placa", "Placa inválida, a mesma deve ter o formato ex: 'AAA-12A2'.")
        );
    }
}
