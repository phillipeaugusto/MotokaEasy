using MotokaEasy.Core.Domain.Entities;

namespace MotokaEasy.Domain.Entities;

public class Configuracao: Entity<Configuracao, Configuracao, Configuracao>
{
    public Configuracao() { }

    public int CodigoParametro { get; set; }
    public string Descricao { get; set; }
    public string Valor { get; set; }
}