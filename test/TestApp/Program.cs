using System;
using System.Threading.Tasks;
using MarketingBox.Integration.Service.Client;
using MarketingBox.Integration.Service.Grpc.Models.Leads;
using MarketingBox.Integration.Service.Grpc.Models.Leads.Contracts;
using ProtoBuf.Grpc.Client;

namespace TestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;

            Console.Write("Press enter to start");
            Console.ReadLine();

            var factory = new IntegrationServiceClientFactory("http://localhost:12347");
            var client = factory.GetIntegrationService();

            var check = await client.CreateAsync(new IntegrationLeadCreateRequest()
            {
                TenantId = "test-tenant-id",
            });

            var testTenant = "Test-Tenant";
            var request = new IntegrationLeadCreateRequest()
            {
                TenantId = testTenant,
            };
            //request.GeneralInfo = new LeadGeneralInfo()
            //{
            //    //Currency = Currency.CHF,
            //    //Email = "email@email.com",
            //    //Password = "sadadadwad",
            //    //Phone = "+79990999999",
            //    //Skype = "skype",
            //    //Type = LeadType.Active,
            //    //Username = "User",
            //    //ZipCode = "414141"
            //};

            //var leadCreated = (await  client.CreateAsync(request)).BrandIntegrationInfo;

            //Console.WriteLine(leadCreated.LeadId);

            //var partnerUpdated = (await client.UpdateAsync(new LeadUpdateRequest()
            //{
            //    LeadId = leadCreated.LeadId,
            //    TenantId = leadCreated.TenantId,
            //    GeneralInfo = request.GeneralInfo,
            //    Sequence = 1
            //})).IntegrationLeadInfo;

            //await client.DeleteAsync(new LeadDeleteRequest()
            //{
            //    LeadId = partnerUpdated.LeadId,
            //});

            //var shouldBeNull =await client.GetAsync(new LeadGetRequest()
            //{
            //    LeadId = partnerUpdated.LeadId,
            //});

            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
