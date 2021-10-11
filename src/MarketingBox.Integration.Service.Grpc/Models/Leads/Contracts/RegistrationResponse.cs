using System.Runtime.Serialization;
using MarketingBox.Integration.Service.Grpc.Models.Common;

namespace MarketingBox.Integration.Service.Grpc.Models.Leads.Contracts
{
    [DataContract]
    public class RegistrationResponse
    {
        [DataMember(Order = 1)]
        public ResultCode Status { get; set; }

        [DataMember(Order = 2)]
        public string Message { get; set; }

        [DataMember(Order = 3)]
        public RegisteredLeadInfo RegisteredLeadInfo { get; set; }

        [DataMember(Order = 4)]
        public string FallbackUrl { get; set; }

        [DataMember(Order = 5)]
        public RegistrationLeadInfo OriginalData { get; set; }

        [DataMember(Order = 100)]
        public Error Error { get; set; }
    }
}