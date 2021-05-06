using System.Threading;
using System.Threading.Tasks;
using QueueWatch.Models;

namespace QueueWatch.Services
{
    public interface INotifier
    {
        Task NotifyAsync(QueueToWatch queueSettings, CancellationToken cancellationToken);
    }
}
