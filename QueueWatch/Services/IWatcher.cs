using System.Threading;
using System.Threading.Tasks;

namespace QueueWatch.Services
{
    public interface IWatcher
    {
        Task CheckAsync(CancellationToken cancellationToken);
    }
}
