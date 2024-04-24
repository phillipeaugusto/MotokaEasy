using MotokaEasy.Core.ConstantsApp;

namespace MotokaEasy.Domain.Shared;

public static class QueueConstants
{
    private const string Queue = ".queue-";
    public static readonly string QueueAtualizarPlacaVeiculo = ApplicationConstants.NameApplication + Queue + "atualizar_placa_veiculo";
    public static readonly string QueueAtualizarPlacaVeiculoErro = ApplicationConstants.NameApplication + Queue + "atualizar_placa_veiculo_erro";
}
