using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR.Client;

namespace GurpsCompanion.Server.Core
{
    public class SignalrRetryPolicy : IRetryPolicy
    {
        private readonly List<TimeSpan> _timeSpans = new()
    {
        TimeSpan.FromMilliseconds(1000),
        TimeSpan.FromMilliseconds(2000),
        TimeSpan.FromMilliseconds(5000)
    };

        public TimeSpan? NextRetryDelay(RetryContext retryContext)
        {
            var retryTimes = Math.Max(0, Math.Min(_timeSpans.Count - 1, (int)retryContext.PreviousRetryCount - 1));
            return _timeSpans[retryTimes];
        }
    }
}
