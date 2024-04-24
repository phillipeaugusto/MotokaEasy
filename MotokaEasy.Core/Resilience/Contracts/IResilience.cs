using System;
using System.Threading.Tasks;

namespace MotokaEasy.Core.Resilience.Contracts;

public interface IResilience
{
    Task ExecuteAsync(Func<Task> task);
    Task<T> ExecuteReturnAsync<T>(Func<Task<T>> task);
    T ExecuteReturn<T>(Func<T> task);
    void Execute(Action action);
}