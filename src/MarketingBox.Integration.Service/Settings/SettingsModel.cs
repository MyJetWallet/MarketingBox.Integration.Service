using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace MarketingBox.Integration.Service.Settings
{
    public class SettingsModel
    {
        [YamlProperty("MarketingBoxIntegrationService.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("MarketingBoxIntegrationService.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("MarketingBoxIntegrationService.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }

        //[YamlProperty("MarketingBoxIntegrationService.PostgresConnectionString")]
        //public string PostgresConnectionString { get; set; }

        [YamlProperty("MarketingBoxIntegrationService.MyNoSqlWriterUrl")]
        public string MyNoSqlWriterUrl { get; set; }

        //[YamlProperty("MarketingBoxIntegrationService.MyNoSqlReaderHostPort")]
        //public string MyNoSqlReaderHostPort { get; set; }

        [YamlProperty("MarketingBoxIntegrationService.MarketingBoxServiceBusHostPort")]
        public string MarketingBoxServiceBusHostPort { get; set; }

        [YamlProperty("MarketingBoxIntegrationService.MarketingBoxIntegrationSimpleBitHostPort")]
        public string MarketingBoxIntegrationSimpleBitHostPort { get; set; }

    }
}
