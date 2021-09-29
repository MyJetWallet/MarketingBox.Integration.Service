using System.ServiceModel;
using System.Threading.Tasks;
using MarketingBox.Integration.Service.Grpc.Models.Leads.Contracts;

namespace MarketingBox.Integration.Service.Grpc
{
    [ServiceContract]
    public interface IIntegrationService
    {
        [OperationContract]
        Task<IntegrationLeadCreateResponse> CreateAsync(IntegrationLeadCreateRequest request);
    }
}
