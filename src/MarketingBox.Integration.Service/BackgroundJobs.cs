using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using MarketingBox.Integration.Service.Messages.Deposits;
using MarketingBox.Integration.Service.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service.Tools;


namespace MarketingBox.Integration.Service
{
    public class BackgroundJobs
    {
        private readonly MyTaskTimer _operationsTimer;
        private readonly IPublisher<DepositUpdateMessage> _publisherLeadUpdated;
        private readonly ILogger<BackgroundJobs> _logger;
        private readonly IDepositUpdateStorage _depositUpdateStorage;

        public BackgroundJobs(IPublisher<DepositUpdateMessage> publisherLeadUpdated,
            ILogger<BackgroundJobs> logger,
            IDepositUpdateStorage depositUpdateStorage)
        {
            _publisherLeadUpdated = publisherLeadUpdated;
            _logger = logger;
            _depositUpdateStorage = depositUpdateStorage;
            _operationsTimer = new MyTaskTimer(nameof(BackgroundJobs), TimeSpan.FromSeconds(10), logger, Process);
            //_operationsTimer.Register("OperationsTimer", async () => { await Process(); });

        }
        public void Start()
        {
            _operationsTimer.Start();
        }

        public void Stop()
        {
            _operationsTimer.Stop();
        }

        private async Task Process()
        {
            try
            {
                var messages = _depositUpdateStorage.GetAll();
                foreach (var operation in messages)
                {
                    var message = operation.Value;
                    await _publisherLeadUpdated.PublishAsync(message);
                    _depositUpdateStorage.Remove(operation.Key);
                }
            }
            catch (Exception e)
            {
                _logger.LogError("IntegrationGrpcService exception {@Message}", e.Message);
            }
        }
    }
}