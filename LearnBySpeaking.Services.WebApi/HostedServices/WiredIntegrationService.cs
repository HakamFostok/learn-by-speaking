using LearnBySpeaking.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LearnBySpeaking.Services.WebApi.HostedServices
{
    public class WiredIntegrationService : BackgroundService
    {
        private const int ONE_HOUR = 1000 * 60 * 60;
        private readonly IServiceProvider _serviceProvider;

        public WiredIntegrationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    using var integrationService = scope.ServiceProvider.GetService<IWiredIntegrationAppService>();
                    await integrationService.Integration();
                }
                catch (Exception)
                {

                }
                finally
                {
                    await Task.Delay(ONE_HOUR);
                }
            }
        }
    }
}
