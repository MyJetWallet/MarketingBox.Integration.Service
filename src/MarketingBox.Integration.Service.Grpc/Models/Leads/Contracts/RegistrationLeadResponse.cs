using System.Runtime.Serialization;
using MarketingBox.Integration.Service.Grpc.Models.Common;

namespace MarketingBox.Integration.Service.Grpc.Models.Leads.Contracts
{
    [DataContract]
    public class RegistrationLeadResponse
    {
        [DataMember(Order = 1)]
        public string Status { get; set; }

        [DataMember(Order = 2)]
        public string Message { get; set; }

        [DataMember(Order = 3)]
        public RegistrationCustomerInfo RegistrationCustomerInfo { get; set; }

        [DataMember(Order = 4)]
        public string FallbackUrl { get; set; }

        [DataMember(Order = 5)]
        public RegistrationLeadInfo OriginalData { get; set; }

        [DataMember(Order = 100)]
        public Error Error { get; set; }

        public static RegistrationLeadResponse Successfully(RegistrationCustomerInfo brandRegistrationCustomerInfo)
        {
            return new RegistrationLeadResponse()
            {
                Status = "successful",
                Message = brandRegistrationCustomerInfo.LoginUrl,
                RegistrationCustomerInfo = brandRegistrationCustomerInfo
            };
        }

        public static RegistrationLeadResponse Failed(Error error, RegistrationLeadInfo originalData)
        {
            return new RegistrationLeadResponse()
            {
                Status = "failed",
                Message = error.Message,
                Error = error,
                OriginalData = originalData
            };
        }
    }
}