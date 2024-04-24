using System;

namespace MotokaEasy.Domain.Events;

public class AtualizarNumeroPlacaVeiculoEvent
{
    public AtualizarNumeroPlacaVeiculoEvent(Guid veiculoId, string placa)
    {
        VeiculoId = veiculoId;
        Placa = placa;
    }

    public Guid VeiculoId { get; set; }
    public string Placa { get; set; }
}