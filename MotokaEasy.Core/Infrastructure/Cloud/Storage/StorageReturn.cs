namespace MotokaEasy.Core.Infrastructure.Cloud.Storage;

public class StorageReturn
{
    public StorageReturn() { }
    public StorageReturn(bool sucesso, string urlArquivo, string mensagem)
    {
        Sucesso = sucesso;
        UrlArquivo = urlArquivo;
        Mensagem = mensagem;
    }

    public bool Sucesso { get; set; }
    public string UrlArquivo { get; set; }
    public string Mensagem { get; set; }
}