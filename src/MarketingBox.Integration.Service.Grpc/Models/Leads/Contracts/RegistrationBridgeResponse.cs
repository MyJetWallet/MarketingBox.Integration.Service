using System.Runtime.Serialization;
using MarketingBox.Integration.Service.Grpc.Models.Common;

namespace MarketingBox.Integration.Service.Grpc.Models.Leads.Contracts
{
    [DataContract]
    public class RegistrationBridgeResponse
    {
        [DataMember(Order = 1)]
        public ResultCode ResultCode { get; set; }

        [DataMember(Order = 2)]
        public string ResultMessage { get; set; }

        [DataMember(Order = 3)]
        public RegisteredLeadInfo RegistrationInfo { get; set; }

        [DataMember(Order = 100)]
        public Error Error { get; set; }
    }
}