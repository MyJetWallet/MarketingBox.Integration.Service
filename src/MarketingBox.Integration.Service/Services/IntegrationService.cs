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
using MarketingBox.Integration.Service.Storage;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Common;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Customers;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Customers.Contracts;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Leads.Contracts;
using Error = MarketingBox.Integration.Service.Grpc.Models.Common.Error;
using RegistrationCustomerInfo = MarketingBox.Integration.Service.Grpc.Models.Leads.RegistrationCustomerInfo;

namespace MarketingBox.Integration.Service.Services
{
    public class IntegrationService : IIntegrationService
    {
        private readonly ILogger<IntegrationService> _logger;
        private readonly IRegisterService _bridgeService;
        private readonly IDepositUpdateStorage _depositUpdateStorage;


        public IntegrationService(ILogger<IntegrationService> logger,
            IRegisterService bridgeService,
            IDepositUpdateStorage depositUpdateStorage
            )
        {
            _logger = logger;
            _bridgeService = bridgeService;
            _depositUpdateStorage = depositUpdateStorage;
        }

        public async Task<RegistrationLeadResponse> RegisterLeadAsync(RegistrationLeadRequest request)
        {
            _logger.LogInformation("Creating new RegistrationLeadInfo {@context}", request); 
            try
            {
                var customerInfo = await _bridgeService.RegisterCustomerAsync(new RegistrationCustomerRequest()
                {
                    Info = new RegistrationLeadInfo()
                        { 
                            Email = request.Info.Email,
                            Password = request.Info.Password,
                            Country = request.Info.Country,
                            FirstName = request.Info.FirstName,
                            Ip = request.Info.Ip,
                            Language = request.Info.Ip,
                            LastName = request.Info.LastName,
                            Phone = request.Info.Phone
                        },
                    LeadId = request.LeadId,
                    TenantId = request.TenantId,
                    AdditionalInfo = new RegistrationLeadAdditionalInfo()
                    {
                        So = request.AdditionalInfo.So,
                        Sub = request.AdditionalInfo.Sub,
                        Sub1 = request.AdditionalInfo.Sub1,
                        Sub2 = request.AdditionalInfo.Sub2,
                        Sub3 = request.AdditionalInfo.Sub3,
                        Sub4 = request.AdditionalInfo.Sub4,
                        Sub5 = request.AdditionalInfo.Sub5,
                        Sub6 = request.AdditionalInfo.Sub6,
                        Sub7 = request.AdditionalInfo.Sub7,
                        Sub8 = request.AdditionalInfo.Sub8,
                        Sub9 = request.AdditionalInfo.Sub9,
                        Sub10 = request.AdditionalInfo.Sub10,
                    },
                    LeadUniqueId = request.LeadUniqueId
                });

                if (customerInfo.Status.Equals("successful", StringComparison.OrdinalIgnoreCase))
                {
                    _depositUpdateStorage.Add(request.LeadUniqueId, new DepositUpdateMessage()
                    {
                        BrandName = request.BrandName,
                        CustomerId = customerInfo.RegistrationInfo.CustomerId,
                        Email = request.Info.Email,
                        TenantId = request.TenantId,
                        Sequence = 0,
                        BrandId = request.BrandId,
                    });
                }

                return MapToGrpc(customerInfo, request);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating lead {@context}", request);

                return new RegistrationLeadResponse() { Error = new Error() { Message = "Internal error", Type = ErrorType.Unknown } };
            }
        }


        private static RegistrationLeadResponse MapToGrpc(RegistrationCustomerResponse brandCustomerInfo,
            RegistrationLeadRequest registrationLeadRequest)
        {
            if (brandCustomerInfo.Status.Equals("successful", StringComparison.OrdinalIgnoreCase))
            {
                return RegistrationLeadResponse.Successfully(new RegistrationCustomerInfo()
                {
                    CustomerId = brandCustomerInfo.RegistrationInfo.CustomerId,
                    LoginUrl = brandCustomerInfo.RegistrationInfo.LoginUrl,
                    Password = registrationLeadRequest.Info.Password,
                    Token = brandCustomerInfo.RegistrationInfo.Token
                });
            }

            return RegistrationLeadResponse.Failed(new Error()
                {
                    Message = brandCustomerInfo.Message,
                    Type = MapError(brandCustomerInfo.Error.Type)
                }, new Grpc.Models.Leads.RegistrationLeadInfo()
                {
                    Email = registrationLeadRequest.Info.Email,
                    Password = registrationLeadRequest.Info.Password,
                    Country = registrationLeadRequest.Info.Country,
                    FirstName = registrationLeadRequest.Info.FirstName,
                    Ip = registrationLeadRequest.Info.Ip,
                    Language = registrationLeadRequest.Info.Ip,
                    LastName = registrationLeadRequest.Info.LastName,
                    Phone = registrationLeadRequest.Info.Phone
                }
            );
        }

        private static ErrorType MapError(RegisterErrorType registerErrorType)
        {
            switch (registerErrorType)
            {
                case RegisterErrorType.InvalidParameter:
                    return ErrorType.InvalidParameter;

                case RegisterErrorType.RegistrationAlreadyExist:
                    return ErrorType.RegistrationAlreadyExist;

                case RegisterErrorType.Unknown:
                    return ErrorType.Unknown;

                default: return ErrorType.Unknown;
            }
        }
    }
}
