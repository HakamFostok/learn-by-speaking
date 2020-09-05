using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LearnBySpeaking.Services.WebApi.HostedServices
{
    public class WiredIntegrationService : BackgroundService
    {
        private readonly HttpClient _httpClient;

        public WiredIntegrationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
