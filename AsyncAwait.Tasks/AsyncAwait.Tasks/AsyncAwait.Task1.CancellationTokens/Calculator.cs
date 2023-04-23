using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.Task1.CancellationTokens;

internal static class Calculator
{
    public static Task<long> CalculateAsync(int n, CancellationToken cancellationToken)
    {
        long sum = 0;

        for (var i = 0; i < n; i++)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine();
                throw new OperationCanceledException($"Task cancelled at index {i}.");
                //cancellationToken.ThrowIfCancellationRequested();
            }
            // i + 1 is to allow 2147483647 (Max(Int32)) 
            sum += (i + 1);
            Thread.Sleep(1000);
        }
        return Task.FromResult(sum);
    }
}
