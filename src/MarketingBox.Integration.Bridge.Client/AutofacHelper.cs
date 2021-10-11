using Autofac;

using MarketingBox.Integration.Service.Grpc;

// ReSharper disable UnusedMember.Global

namespace MarketingBox.Integration.Bridge.Client
{
    public static class AutofacHelper
    {
        public static void RegisterSimpleTradingBridgeClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new BridgeServiceClientFactory(grpcServiceUrl);

            builder.RegisterInstance(factory.GetBridgeService()).As<IBridgeService>().SingleInstance();
        }
    }
}
