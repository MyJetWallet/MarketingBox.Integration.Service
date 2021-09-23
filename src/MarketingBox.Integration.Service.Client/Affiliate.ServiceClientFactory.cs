using JetBrains.Annotations;
using MarketingBox.Integration.Service.Grpc;
using MyJetWallet.Sdk.Grpc;

namespace MarketingBox.Integration.Service.Client
{
    [UsedImplicitly]
    public class IntegrationServiceClientFactory: MyGrpcClientFactory
    {
        public IntegrationServiceClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }

        public IIntegrationService GetPartnerService() => CreateGrpcService<IIntegrationService>();
    }
}
