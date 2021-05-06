using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using QueueWatch.Services;

namespace QueueWatch.TimerFunctions
{
    public class Watcher
    {
        private readonly IEnumerable<IWatcher> _queuesToWatch;
        private readonly ILogger<Watcher> _logger;

        public Watcher(IEnumerable<IWatcher> queuesToWatch, ILogger<Watcher> logger)
        {
            _queuesToWatch = queuesToWatch ?? throw new ArgumentNullException(nameof(queuesToWatch));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [FunctionName("Watcher")]
        [Singleton]
        public async Task RunAsync([TimerTrigger("0 0 */1 * * *", RunOnStartup = true)] TimerInfo timer, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Running QueueWatch...");
            await Task.WhenAll(_queuesToWatch.Select(queueToWatch => queueToWatch.CheckAsync(cancellationToken)));
        }
    }
}
