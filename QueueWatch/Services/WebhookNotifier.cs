using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QueueWatch.Models;

namespace QueueWatch.Services
{
    public class WebhookNotifier : INotifier
    {
        private readonly ILogger<WebhookNotifier> _logger;
        private readonly Uri _uri;
        private readonly HttpClient _httpClient;

        public WebhookNotifier(ILogger<WebhookNotifier> logger,
                               IHttpClientFactory httpClientFactory,
                               Uri uri)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uri = uri ?? throw new ArgumentNullException(nameof(uri));
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task NotifyAsync(QueueToWatch queueSettings, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Notifying about: {queue}", queueSettings);

            await _httpClient.PostAsync(_uri, new StringContent(JsonConvert.SerializeObject(new { text = queueSettings.Status })));
        }
    }
}
