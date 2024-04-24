namespace MotokaEasy.Core.ConstantsApp;

public static class QueueMessageBrokerConstants
{
    private const string Queue = ".queue-";
    public static readonly string QueueExtratorDeDados = ApplicationConstants.NameApplication + Queue + "extrator_dados_operacao";
    public static readonly string QueueExtratorDeDadosErro = ApplicationConstants.NameApplication + Queue + "extrator_dados_operacao_error";

}
