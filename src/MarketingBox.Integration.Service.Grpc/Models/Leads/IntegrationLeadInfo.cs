using System.Runtime.Serialization;

namespace MarketingBox.Integration.Service.Grpc.Models.Leads
{
    [DataContract]
    public class IntegrationLeadInfo
    {
        [DataMember(Order = 1)]
        public long LeadId { get; set; }

        [DataMember(Order = 2)]
        public string UniqueId { get; set; }

        [DataMember(Order = 3)]
        public LeadGeneralInfo GeneralInfo { get; set; }

        [DataMember(Order = 4)]
        public LeadAdditionalIntegrationInfo AdditionalIntegrationInfo { get; set; }
    }
}
