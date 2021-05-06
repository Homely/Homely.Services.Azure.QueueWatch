using System;
using System.IO;
using System.Net.Http;
using Homely.Storage.Queues;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using QueueWatch;
using QueueWatch.Configuration;
using QueueWatch.Models;
using QueueWatch.Services;

[assembly: FunctionsStartup(typeof(Startup))]

namespace QueueWatch
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Core services
            var provider = builder.Services.BuildServiceProvider(true);
            var hostingEnvironment = provider.GetRequiredService<IHostingEnvironment>();
            var binFolderPath = Path.GetDirectoryName(typeof(Startup).Assembly.Location);
            var webRootFolder = Directory.GetParent(binFolderPath);
            var webRootFolderPath = webRootFolder.FullName;
            var services = builder.Services;

            // Configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(webRootFolderPath)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: false, reloadOnChange: false)                
                .AddEnvironmentVariables()
                .AddUserSecrets<Startup>()
                .Build();

            services.AddSingleton(typeof(IConfiguration), configuration);

            // Logging            
            services.AddLogging();

            // Queues
            var watching = configuration.GetSection("Watching").Get<string[]>();
            foreach (var watch in watching)
            {
                var config = new QueueConfig();
                configuration.GetSection($"Settings:{watch}").Bind(config);

                services.AddSingleton<IWatcher>(sp => new Watcher(sp.GetService<ILogger<Watcher>>(),
                                                                  new QueueToWatch(new AzureQueue(config.ConnectionString,
                                                                                                   config.Name,
                                                                                                   sp.GetService<ILogger<AzureQueue>>()),
                                                                                    config),
                                                                  sp.GetService<INotifier>()));
            }

            // HTTP.
            services.AddHttpClient();

            // Notification.
            services.AddSingleton<INotifier>(sp => new WebhookNotifier(sp.GetService<ILogger<WebhookNotifier>>(),
                                                                       sp.GetService<IHttpClientFactory>(),
                                                                       configuration.GetValue<Uri>("WebhookUri")));
        }
    }
}
