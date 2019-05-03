using System.Configuration;

namespace ZigmaWeb.Configuration
{
    public static class AppConfigurationManager
    {
        public static string SenderNetworkCredentialUserName
        {
            get
            {
                return ConfigurationManager.AppSettings["SenderNetworkCredentialUserName"];
            }
        }

        public static string SenderNetworkCredentialPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["SenderNetworkCredentialPassword"];
            }
        }

        public static string SmtpClientHost
        {
            get
            {
                return ConfigurationManager.AppSettings["SmtpClientHost"];
            }
        }

        public static int SmtpClientPort
        {
            get { return int.Parse(ConfigurationManager.AppSettings["SmtpClientPort"]); }
        }

        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
