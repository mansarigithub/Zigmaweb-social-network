using System.Configuration;
using System.IO;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Common.Configuration
{
    public static class AppDirectoryManager
    {
        public static string GetUserProfileImageDirectory(int userId)
        {
            return $"{AppConfigurationManager.AppDirectory}\\Users\\{userId}\\ProfileImage";
        }

        public static void CreateUserProfileImageDirectory(int userId)
        {
            var path = $"{AppConfigurationManager.AppDirectory}\\Users\\{userId}\\ProfileImage";
            Directory.CreateDirectory(path);
        }

    }
}
