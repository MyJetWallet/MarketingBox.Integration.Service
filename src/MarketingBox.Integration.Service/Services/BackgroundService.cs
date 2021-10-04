using System;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using MarketingBox.Integration.Service.Messages.Deposits;
using MarketingBox.Integration.Service.Storage;
using MarketingBox.Registration.Service.Grpc;
using MarketingBox.Registration.Service.Grpc.Models.Deposits.Contracts;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service.Tools;

namespace MarketingBox.Integration.Service.Services
{
    public class BackgroundService
    {
        private readonly MyTaskTimer _operationsTimer;
        //private readonly IPublisher<DepositUpdateMessage> _publisherLeadUpdated;
        private readonly ILogger<BackgroundService> _logger;
        private readonly IDepositUpdateStorage _depositUpdateStorage;
        private readonly IDepositService _depositRegistrationService;

        public BackgroundService(IPublisher<DepositUpdateMessage> publisherLeadUpdated,
            ILogger<BackgroundService> logger,
            IDepositUpdateStorage depositUpdateStorage, 
            IDepositService depositRegistrationService)
        {
            //_publisherLeadUpdated = publisherLeadUpdated;
            _logger = logger;
            _depositUpdateStorage = depositUpdateStorage;
            _depositRegistrationService = depositRegistrationService;
            _operationsTimer = new MyTaskTimer(nameof(BackgroundService), TimeSpan.FromSeconds(10), logger, Process);
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
                    var storeResponse = await _depositRegistrationService.CreateDepositAsync(new DepositCreateRequest()
                    {
                        CustomerId = message.CustomerId,
                        Email = message.Email,
                        BrandName = message.BrandName,
                        BrandId = message.BrandId,
                        TenantId = message.TenantId,
                        CreatedAt = DateTimeOffset.UtcNow,
                    });
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