using System.ServiceModel;
using System.Threading.Tasks;
using MarketingBox.Integration.Service.Grpc.Models.Leads.Contracts;
using MarketingBox.Integration.Service.Grpc.Models.Reporting;

namespace MarketingBox.Integration.Service.Grpc
{
    [ServiceContract]
    public interface IBridgeService
    {
        [OperationContract]
        Task<RegistrationBridgeResponse> RegisterCustomerAsync(RegistrationBridgeRequest bridgeRequest);
        Task<BridgeCountersResponse> GetBridgeCountersPerPeriodAsync(CountersRequest request);
        Task<RegistrationsResponse> GetRegistrationsPerPeriodAsync(RegistrationsRequest request);
        Task<DepositsResponse> GetDepositsPerPeriodAsync(DepositsRequest request);
    }
}
