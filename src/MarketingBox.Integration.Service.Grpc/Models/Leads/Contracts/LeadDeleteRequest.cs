using System.Runtime.Serialization;

namespace MarketingBox.Integration.Service.Grpc.Models.Leads.Messages
{
    [DataContract]
    public class LeadDeleteRequest
    {
        [DataMember(Order = 1)]
        public long LeadId { get; set; }
    }
}
