using System.Runtime.Serialization;

namespace MarketingBox.Integration.Service.Grpc.Models.Leads
{
    [DataContract]
    public class LeadBrandIntegrationInfo
    {
        [DataMember(Order = 1)]
        public long BrandId { get; set; }

        [DataMember(Order = 2)]
        public string Brand { get; set; }
    }
}