using MarketingBox.Integration.Service.Grpc.Models.Leads;
using System.ServiceModel;
using System.Threading.Tasks;
using MarketingBox.Integration.Service.Grpc.Models.Leads.Contracts;
using MarketingBox.Integration.Service.Grpc.Models.Leads.Messages;
using MarketingBox.Integration.Service.Grpc.Models.Leads.Requests;

namespace MarketingBox.Integration.Service.Grpc
{
    [ServiceContract]
    public interface IIntegrationService
    {
        [OperationContract]
        Task<LeadCreateResponse> CreateAsync(LeadCreateRequest request);

        [OperationContract]
        Task<LeadCreateResponse> UpdateAsync(LeadUpdateRequest request);

        [OperationContract]
        Task<LeadCreateResponse> GetAsync(LeadGetRequest request);

        [OperationContract]
        Task<LeadCreateResponse> DeleteAsync(LeadDeleteRequest request);
    }
}
