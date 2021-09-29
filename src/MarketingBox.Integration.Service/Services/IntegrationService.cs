using DotNetCoreDecorators;
using MarketingBox.Integration.Postgres;
using MarketingBox.Integration.Service.Grpc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using MarketingBox.Integration.Postgres.Entities.Lead;
using MarketingBox.Integration.Service.Grpc.Models.Common;
using MarketingBox.Integration.Service.Grpc.Models.Leads.Contracts;
using MarketingBox.Integration.Service.Messages.Deposits;

namespace MarketingBox.Integration.Service.Services
{
    public class IntegrationService : IIntegrationService
    {
        private readonly ILogger<IntegrationService> _logger;
        private readonly DbContextOptionsBuilder<DatabaseContext> _dbContextOptionsBuilder;
        private readonly IPublisher<DepositUpdateMessage> _publisherLeadUpdated;
        //private readonly IMyNoSqlServerDataWriter<LeadNoSql> _myNoSqlServerDataWriter;

        public IntegrationService(ILogger<IntegrationService> logger,
            DbContextOptionsBuilder<DatabaseContext> dbContextOptionsBuilder,
            IPublisher<DepositUpdateMessage> publisherLeadUpdated
            //IMyNoSqlServerDataWriter<LeadNoSql> myNoSqlServerDataWriter
            )
        {
            _logger = logger;
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
            _publisherLeadUpdated = publisherLeadUpdated;
            //_myNoSqlServerDataWriter = myNoSqlServerDataWriter;
        }

        public async Task<IntegrationLeadCreateResponse> CreateAsync(Service.Grpc.Models.Leads.Contracts.IntegrationLeadCreateRequest request)
        {
            _logger.LogInformation("Creating new IntegrationLeadInfo {@context}", request);
            using var ctx = new DatabaseContext(_dbContextOptionsBuilder.Options);

            try
            {

                //await _publisherLeadUpdated.PublishAsync(MapToMessage(leadEntity));
                //_logger.LogInformation("Sent partner update to service bus {@context}", request);

                //var nosql = MapToNoSql(leadEntity);
                //await _myNoSqlServerDataWriter.InsertOrReplaceAsync(nosql);
                //_logger.LogInformation("Sent partner update to MyNoSql {@context}", request);

                var brandInfo = await BrandRegisterAsync(null);

                return MapToGrpc(null, null);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating lead {@context}", request);

                return new IntegrationLeadCreateResponse() { Error = new Error() { Message = "Internal error", Type = ErrorType.Unknown } };
            }
        }

        public async Task<Grpc.Models.Leads.LeadBrandIntegrationInfo> BrandRegisterAsync(LeadEntity leadEntity)
        {
            string brandLoginUrl = @"https://trading-test.handelpro.biz/lpLogin/6DB5D4818181B806DBF7B19EBDC5FD97F1B82759077317B6481BC883F071783DBEF568426B81DF43044E326C26437E097F21A2484110D13420E9EC6E44A1B2BE?lang=PL";
            string brandName = "Monfex";
            string brandCustomerId = "02537c06cab34f62931c263bf3480959";
            string customerEmail = "yuriy.test.2020.09.22.01@mailinator.com";
            string brandToken = "6DB5D4818181B806DBF7B19EBDC5FD97F1B82759077317B6481BC883F071783DBEF568426B81DF43044E326C26437E097F21A2484110D13420E9EC6E44A1B2BE";

            var brandInfo = new Grpc.Models.Leads.LeadBrandIntegrationInfo()
            {
                //Status = "successful",
                //Data = new LeadBrandRegistrationInfo()
                //{
                //    Email = customerEmail, //leadEntity.Email,
                //    UniqueId = leadEntity.LeadId.ToString(),
                //    LoginUrl = brandLoginUrl,
                //    Broker = brandName,
                //    CustomerId = brandCustomerId,
                //    Token = brandToken
                //}
            };
            await Task.Delay(1000);
            return brandInfo;
        }


        private static IntegrationLeadCreateResponse MapToGrpc(LeadEntity leadEntity, 
            Grpc.Models.Leads.LeadBrandRegistrationInfo brandInfo)
        {
            //TODO: Remove
            return new IntegrationLeadCreateResponse() 
            {
                //Status = ,
                //FallbackUrl = String.Empty,
                //Message = .Data.LoginUrl,
                //Error = null,
                //OriginalData = null,
            };
        }

        private static DepositUpdateMessage MapToMessage(LeadEntity leadEntity)
        {
            return new DepositUpdateMessage()
            {
                //TenantId = leadEntity.TenantId,
                //AffiliateId = leadEntity.LeadId,
                //GeneralInfo = new Messages.Partners.PartnerGeneralInfo()
                //{
                //    //CreatedAt = leadEntity.BrandIntegrationInfo.CreatedAt.UtcDateTime,
                //    //Email = leadEntity.BrandIntegrationInfo.Email,
                //    ////Password = leadEntity.BrandIntegrationInfo.Password,
                //    //Phone = leadEntity.BrandIntegrationInfo.Phone,
                //    //Role = leadEntity.BrandIntegrationInfo.Role.MapEnum<Messages.Partners.PartnerRole>(),
                //    //Skype = leadEntity.BrandIntegrationInfo.Skype,
                //    //Type = leadEntity.BrandIntegrationInfo.Type.MapEnum<Messages.Partners.PartnerState>(),
                //    //Username = leadEntity.BrandIntegrationInfo.Username,
                //    //ZipCode = leadEntity.BrandIntegrationInfo.ZipCode
                //}
            };
        }

        //private static LeadNoSql MapToNoSql(LeadEntity leadEntity)
        //{
        //    return LeadNoSql.Create(
        //        leadEntity.TenantId,
        //        leadEntity.LeadId,
        //        new MyNoSql.Leads.LeadGeneralInfo()
        //        {
        //            CreatedAt = leadEntity.CreatedAt,
        //            Email = leadEntity.Email,
        //            Username = leadEntity.FirstName + " " + leadEntity.LastName
        //        }
        //        );
        //}
    }
}
