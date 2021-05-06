using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QueueWatch.Models;

namespace QueueWatch.Services
{
    public class Watcher : IWatcher
    {
        private readonly ILogger<Watcher> _logger;
        private readonly QueueToWatch _queueToWatch;
        private readonly INotifier _notifier;

        public Watcher(ILogger<Watcher> logger,
                       QueueToWatch queueToWatch,
                       INotifier notifier)
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            _queueToWatch = queueToWatch ?? throw new System.ArgumentNullException(nameof(queueToWatch));
            _notifier = notifier ?? throw new System.ArgumentNullException(nameof(notifier));
        }

        public async Task CheckAsync(CancellationToken cancellationToken)
        {
            var isOverThreshold = await _queueToWatch.IsOverThresoldAsync(cancellationToken);

            _logger.LogDebug(_queueToWatch.Status);

            if (isOverThreshold)
            {
                _logger.LogWarning("Queue is past threshold. Calling notifier...");
                await _notifier.NotifyAsync(_queueToWatch, cancellationToken);
            }
        }
    }
}
