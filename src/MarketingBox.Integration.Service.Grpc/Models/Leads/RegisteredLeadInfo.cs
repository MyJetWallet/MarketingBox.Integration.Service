using System.Runtime.Serialization;
using Destructurama.Attributed;

namespace MarketingBox.Integration.Service.Grpc.Models.Leads
{
    [DataContract]
    public class 
        RegisteredLeadInfo
    {
        [DataMember(Order = 1)]
        public string CustomerId { get; set; }

        [DataMember(Order = 2)]
        public string Token { get; set; }

        [DataMember(Order = 3)]
        public string LoginUrl { get; set; }
    }
}