using System.Runtime.Serialization;

namespace MarketingBox.Integration.Service.Grpc.Models.Leads.Messages
{
    [DataContract]
    public class LeadGetRequest 
    {
        [DataMember(Order = 1)]
        public long LeadId { get; set; }
    }
}