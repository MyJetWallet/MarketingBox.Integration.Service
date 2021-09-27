using System.Runtime.Serialization;
using MarketingBox.Integration.Service.Messages.Common;

namespace MarketingBox.Integration.Service.Messages.Deposits
{
    [DataContract]
    public class DepositUpdateMessage
    {
        [DataMember(Order = 1)]
        public string TenantId { get; set; }

        [DataMember(Order = 2)]
        public long LeadId { get; set; }

        [DataMember(Order = 3)]
        public string CustomerId { get; set; }

        [DataMember(Order = 4)]
        public decimal Amount { get; set; }

        [DataMember(Order = 5)]
        public Currency Currency { get; set; }

        [DataMember(Order = 6)]
        public long Sequence { get; set; }
    }
}
