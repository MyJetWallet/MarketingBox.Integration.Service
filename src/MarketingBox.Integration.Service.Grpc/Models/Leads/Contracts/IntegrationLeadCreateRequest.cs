using System.Runtime.Serialization;

namespace MarketingBox.Integration.Service.Grpc.Models.Leads.Contracts
{
    [DataContract]
    public class IntegrationLeadCreateRequest
    {
        [DataMember(Order = 1)]
        public string TenantId { get; set; }

        [DataMember(Order = 2)]
        public IntegrationLeadInfo IntegrationLeadInfo { get; set; }

        [DataMember(Order = 3)]
        public LeadBrandIntegrationInfo BrandIntegrationInfo { get; set; }
    }
}
