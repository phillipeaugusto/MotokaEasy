using System.Threading.Tasks;
using MotokaEasy.Core.Shared.Contracts;

namespace MotokaEasy.Core.Shared.Result;

public static class Result
{
    public static Task<IResultGeneric> ResultAsync(bool success, string message = "", object data = null)
    {
        return Task.FromResult<IResultGeneric>(new GenericResult(success, message, data));
    }
}