using System.Runtime.Serialization;

namespace MarketingBox.Integration.Service.Grpc.Models.Leads
{
    [DataContract]
    public class LeadAuthInfo
    {
        [DataMember(Order = 1)]
        public string AffiliateId { get; set; }

        [DataMember(Order = 2)]
        public string ApiKey { get; set; }
    }
}