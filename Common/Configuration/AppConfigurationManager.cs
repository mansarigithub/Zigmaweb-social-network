using System;
using System.Configuration;

namespace ZigmaWeb.Common.Configuration
{
    public static class AppConfigurationManager
    {
        static TimeZoneInfo _localSystemTimeZone;

        public static string AppDirectory => ConfigurationManager.AppSettings["AppDirectory"];

        public static string GetUserMediaDirectory()
        {
            return $"{ConfigurationManager.AppSettings["AppStorageDirectory"]}";
        }

        public static string SenderNetworkCredentialUserName => ConfigurationManager.AppSettings["SenderNetworkCredentialUserName"];

        public static string SenderNetworkCredentialPassword => ConfigurationManager.AppSettings["SenderNetworkCredentialPassword"];

        public static string SmtpClientHost => ConfigurationManager.AppSettings["SmtpClientHost"];

        public static int SmtpClientPort => int.Parse(ConfigurationManager.AppSettings["SmtpClientPort"]);

        public static int LatestArticlesNumber => int.Parse(ConfigurationManager.AppSettings["LatestArticlesNumber"]);
        public static int TopArticlesNumber => int.Parse(ConfigurationManager.AppSettings["TopArticlesNumber"]);
        public static TimeZoneInfo LocalSystemTimeZone => _localSystemTimeZone ??
                                                          (_localSystemTimeZone =
                                                              TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["LocalSystemTimeZoneId"]));

        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
