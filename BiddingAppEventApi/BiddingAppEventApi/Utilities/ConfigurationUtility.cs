using System.Configuration;

namespace WebAppEventApi.Utilities
{
    // This class provides a wrapper instance for the ConfigurationManager
    public class ConfigurationUtility : IConfigurationUtility
    {
        public string ApiKey => ConfigurationManager.AppSettings["apiKey"];
    }
}
