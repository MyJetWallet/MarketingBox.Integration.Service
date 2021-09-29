using System.Runtime.Serialization;
using MarketingBox.Integration.Service.Grpc.Models.Common;

namespace MarketingBox.Integration.Service.Grpc.Models.Leads.Contracts
{
    [DataContract]
    public class IntegrationLeadCreateResponse
    {
        [DataMember(Order = 1)]
        public string Status { get; set; }

        [DataMember(Order = 2)]
        public string Message { get; set; }

        [DataMember(Order = 3)]
        public LeadBrandRegistrationInfo RegistrationInfo { get; set; }

        [DataMember(Order = 4)]
        public string FallbackUrl { get; set; }

        [DataMember(Order = 5)]
        public LeadGeneralInfo OriginalData { get; set; }

        [DataMember(Order = 100)]
        public Error Error { get; set; }

        public static IntegrationLeadCreateResponse Successfully(LeadBrandRegistrationInfo brandRegistrationInfo)
        {
            return new IntegrationLeadCreateResponse()
            {
                Status = "successful",
                Message = brandRegistrationInfo.LoginUrl,
                RegistrationInfo = brandRegistrationInfo
            };
        }

        public static IntegrationLeadCreateResponse Failed(Error error, LeadGeneralInfo originalData)
        {
            return new IntegrationLeadCreateResponse()
            {
                Status = "failed",
                Message = error.Message,
                Error = error,
                OriginalData = originalData
            };
        }
    }
}