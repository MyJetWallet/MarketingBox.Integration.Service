using Autofac;
using MarketingBox.Integration.Bridge.Client;
using MarketingBox.Integration.Service.Services;
using MarketingBox.Integration.Service.Storage;
using MarketingBox.Registration.Service.Client;

namespace MarketingBox.Integration.Service.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSimpleTradingBridgeClient(Program.Settings.IntegrationMonfexBridgeUrl);
            builder.RegisterType<DepositUpdateStorage>().As<IDepositUpdateStorage>().SingleInstance();
            builder.RegisterType<BackgroundService>().SingleInstance().AutoActivate().AsSelf();
            builder.RegisterRegistrationServiceClient(Program.Settings.RegistrationServiceUrl);
        }
    }
}
