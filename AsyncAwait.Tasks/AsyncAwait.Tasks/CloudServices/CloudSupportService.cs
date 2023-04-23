using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudServices.Interfaces;

namespace CloudServices;

/// <summary>
/// This class emulates methods accessing to some cloud services.
/// No changes are needed here.
/// </summary>
public class CloudSupportService : ISupportService
{
    private static readonly ConcurrentDictionary<string, string> SupportRequests = new();

    private static readonly Random random = new();

    #region public methods

    public async Task RegisterSupportRequestAsync(string requestInfo)
    {
        var lastRequestedOn = DateTime.UtcNow.ToShortDateString();
        SupportRequests.AddOrUpdate(requestInfo, lastRequestedOn, (k, v) => lastRequestedOn);
        await Task.Delay(100); // emulates assistance request registration.
    }

    public Task<string> GetSupportInfoAsync(string requestInfo)
    {
        var nextRandom = random.Next(2); // emulation of support availability
        var availableSupportRequest = nextRandom == 0
            ? SupportRequests.GetValueOrDefault(requestInfo)
            : null;

        return string.IsNullOrEmpty(availableSupportRequest)
            ? Task.FromResult("Support not available. Please try a bit later.")
            : Task.FromResult($"Your support request id: {requestInfo}_{availableSupportRequest}. Please check your email for more details.");
    }

    #endregion
}
