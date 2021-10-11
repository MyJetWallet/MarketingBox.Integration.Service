using System.Runtime.Serialization;

namespace MarketingBox.Integration.Service.Grpc.Models.Leads.Contracts
{
    [DataContract]
    public class RegistrationBridgeRequest
    {
        [DataMember(Order = 1)]
        public RegistrationLeadInfo Info { get; set; }

        [DataMember(Order = 2)]
        public RegistrationLeadAdditionalInfo AdditionalInfo { get; set; }
    }
}
