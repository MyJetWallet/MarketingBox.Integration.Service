using DotNetCoreDecorators;
using MarketingBox.Integration.Postgres;
using MarketingBox.Integration.Service.Grpc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using MarketingBox.Integration.Postgres.Entities.Lead;
using MarketingBox.Integration.Service.Grpc.Models.Common;
using MarketingBox.Integration.Service.Grpc.Models.Leads;
using MarketingBox.Integration.Service.Grpc.Models.Leads.Contracts;
using MarketingBox.Integration.Service.Messages.Deposits;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Leads.Contracts;

namespace MarketingBox.Integration.Service.Services
{
    public class IntegrationService : IIntegrationService
    {
        private readonly ILogger<IntegrationService> _logger;
        //private readonly DbContextOptionsBuilder<DatabaseContext> _dbContextOptionsBuilder;
        private readonly IPublisher<DepositUpdateMessage> _publisherLeadUpdated;
        private readonly IBridgeService _bridgeService;
        private readonly IDepositUpdateStorage _depositUpdateStorage;


        public IntegrationService(ILogger<IntegrationService> logger,
            //DbContextOptionsBuilder<DatabaseContext> dbContextOptionsBuilder,
            IPublisher<DepositUpdateMessage> publisherLeadUpdated,
            IBridgeService bridgeService,
            IDepositUpdateStorage depositUpdateStorage
            )
        {
            _logger = logger;
            //_dbContextOptionsBuilder = dbContextOptionsBuilder;
            _publisherLeadUpdated = publisherLeadUpdated;
            _bridgeService = bridgeService;
            _depositUpdateStorage = depositUpdateStorage;
        }

        public async Task<RegistrationLeadResponse> RegisterLeadAsync(RegistrationLeadRequest request)
        {
            _logger.LogInformation("Creating new RegistrationLeadInfo {@context}", request); 
            try
            {
                //await _publisherLeadUpdated.PublishAsync(MapToMessage(leadEntity));
                //_logger.LogInformation("Sent partner update to service bus {@context}", request);

                //var nosql = MapToNoSql(leadEntity);
                //await _myNoSqlServerDataWriter.InsertOrReplaceAsync(nosql);
                //_logger.LogInformation("Sent partner update to MyNoSql {@context}", request);

                var customerInfo = await _bridgeService.RegisterCustomerAsync(new RegistrationCustomerRequest());
                _depositUpdateStorage.Add(request.LeadUniqueId, new DepositUpdateMessage()
                {
                    BrandName = request.BrandName,
                    CustomerId = customerInfo.RegistrationInfo.CustomerId,
                    Email = request.Info.Email,
                    TenantId = request.TenantId,
                    Sequence = 0,
                    BrandId = request.BrandId
                });


                //using var ctx = new DatabaseContext(_dbContextOptionsBuilder.Options);
                return MapToGrpc(customerInfo);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating lead {@context}", request);

                return new RegistrationLeadResponse() { Error = new Error() { Message = "Internal error", Type = ErrorType.Unknown } };
            }
        }


        private static RegistrationLeadResponse MapToGrpc(RegistrationCustomerResponse brandCustomerInfo)
        {
            //if(brandCustomerInfo.Status.Equals("successful", StringComparison.OrdinalIgnoreCase))
            //{

                return RegistrationLeadResponse.Successfully(new RegistrationCustomerInfo()
                {
                    CustomerId = brandCustomerInfo.RegistrationInfo.CustomerId,
                    LoginUrl = brandCustomerInfo.RegistrationInfo.LoginUrl,
                    Password = brandCustomerInfo.RegistrationInfo.Password,
                    Token = brandCustomerInfo.RegistrationInfo.Token,
                });
            //}

            //return RegistrationLeadResponse.Failed(new Error()
            //{
            //    Message = brandCustomerInfo.Error.Message,
            //    Type = ErrorType.Unknown// brandCustomerInfo.Error.Type
            //}, new RegistrationLeadInfo()
            //{
                
            //});

        }

        private static DepositUpdateMessage MapToMessage(LeadEntity leadEntity)
        {
            return new DepositUpdateMessage()
            {
                //TenantId = leadEntity.TenantId,
                //AffiliateId = leadEntity.LeadId,
                //Info = new Messages.Partners.PartnerGeneralInfo()
                //{
                //    //CreatedAt = leadEntity.RegistrationBrandInfo.CreatedAt.UtcDateTime,
                //    //Email = leadEntity.RegistrationBrandInfo.Email,
                //    ////Password = leadEntity.RegistrationBrandInfo.Password,
                //    //Phone = leadEntity.RegistrationBrandInfo.Phone,
                //    //Role = leadEntity.RegistrationBrandInfo.Role.MapEnum<Messages.Partners.PartnerRole>(),
                //    //Skype = leadEntity.RegistrationBrandInfo.Skype,
                //    //Type = leadEntity.RegistrationBrandInfo.Type.MapEnum<Messages.Partners.PartnerState>(),
                //    //Username = leadEntity.RegistrationBrandInfo.Username,
                //    //ZipCode = leadEntity.RegistrationBrandInfo.ZipCode
                //}
            };
        }

        //private static LeadNoSql MapToNoSql(LeadEntity leadEntity)
        //{
        //    return LeadNoSql.Create(
        //        leadEntity.TenantId,
        //        leadEntity.LeadId,
        //        new MyNoSql.Leads.RegistrationLeadInfo()
        //        {
        //            CreatedAt = leadEntity.CreatedAt,
        //            Email = leadEntity.Email,
        //            Username = leadEntity.FirstName + " " + leadEntity.LastName
        //        }
        //        );
        //}
    }
}
