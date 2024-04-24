using System;
using System.Collections.Generic;
using Polly;
using Polly.Retry;

namespace MotokaEasy.Core.Resilience.Polly;

public class ResiliencePollyFactory
{
    public ResiliencePollyFactory() { }

    private  IEnumerable<TimeSpan> ReturnListTime() =>  new [] {TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10)};

    private void ConfigureConsole(TimeSpan span, int retryCount)
    {
        var previousBackgroundColor = Console.BackgroundColor;
        var previousForegroundColor = Console.ForegroundColor;
                        
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.White;
                        
        Console.Out.WriteLineAsync($" ***** {DateTime.Now:HH:mm:ss} | " +
                                   $"Retentativa: {retryCount} | " +
                                   $"Tempo de Espera em segundo(s): {span.TotalSeconds} **** ");
                        
        Console.BackgroundColor = previousBackgroundColor;
        Console.ForegroundColor = previousForegroundColor;
    }
    public AsyncRetryPolicy ReturnRetryPolicyAsync()
    {
        return Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                sleepDurations: ReturnListTime(),
                onRetry: (_, span, retryCount, _) => { ConfigureConsole(span, retryCount);});
    }

    public RetryPolicy ReturnRetryPolicy()
    {
        return Policy
            .Handle<Exception>()
            .WaitAndRetry(
                sleepDurations: ReturnListTime(),
                onRetry: (_, span, retryCount, _) => {ConfigureConsole(span, retryCount);});
    }
}