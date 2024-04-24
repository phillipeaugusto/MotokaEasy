using System.Threading;
using System.Threading.Tasks;
using MotokaEasy.Core.Infrastructure.Repositories.Contracts;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Domain.Repositories;

public interface IConfiguracaoRepository: IRepository<Configuracao, Configuracao>
{
    Task<string> RetornarValorParametroAsync(int codigoParametro, CancellationToken ct);
}