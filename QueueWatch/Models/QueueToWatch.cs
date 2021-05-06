using System.Threading;
using System.Threading.Tasks;
using Homely.Storage.Queues;
using QueueWatch.Configuration;

namespace QueueWatch.Models
{
    public class QueueToWatch
    {
        private int _currentCount;
        private readonly QueueConfig _config;
        private readonly IQueue _queue;

        public QueueToWatch(IQueue queue,
                            QueueConfig config)
        {
            _queue = queue ?? throw new System.ArgumentNullException(nameof(queue));
            _config = config ?? throw new System.ArgumentNullException(nameof(config));
        }

        public async Task<bool> IsOverThresoldAsync(CancellationToken cancellationToken)
        {
            _currentCount = await _queue.GetMessageCountAsync(cancellationToken);

            if (_currentCount > _config.Threshold)
            {
                return true;
            }

            return false;
        }

        public string Status => $"Name: {_queue.Name}, Count: {_currentCount}, Threshold: {_config.Threshold}";
    }
}
