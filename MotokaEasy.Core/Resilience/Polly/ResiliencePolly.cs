using System;
using System.Threading.Tasks;
using MotokaEasy.Core.Resilience.Contracts;
using Polly.Retry;

namespace MotokaEasy.Core.Resilience.Polly;

public class ResiliencePolly: IResilience
{
    private readonly AsyncRetryPolicy _retryPolicyAsync;
    private readonly RetryPolicy _retryPolicy;
    public ResiliencePolly()
    {
        var resilience = new ResiliencePollyFactory();
        _retryPolicyAsync = resilience.ReturnRetryPolicyAsync();
        _retryPolicy = resilience.ReturnRetryPolicy();
    }

    public async Task ExecuteAsync(Func<Task> task)
    {
        await _retryPolicyAsync.ExecuteAsync(task);
    }

    public Task<T> ExecuteReturnAsync<T>(Func<Task<T>> task)
    {
        return _retryPolicyAsync.ExecuteAsync(task);
    }

    public T ExecuteReturn<T>(Func<T> task)
    {
        return _retryPolicy.Execute(task);
    }

    public void Execute(Action action)
    {
        _retryPolicy.Execute(action);
    }
}