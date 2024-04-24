using System.Threading;
using System.Threading.Tasks;
using MotokaEasy.Core.Domain.Contracts;
using MotokaEasy.Core.Shared.Contracts;

namespace MotokaEasy.Core.Services.Contracts;

public partial interface IService<in T> where T : IValidator
{
    Task<IResultGeneric> Service(T action, CancellationToken ct);
        
}