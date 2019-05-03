using System.Configuration;
using System.IO;
using ZigmaWeb.Common.Enum;
using System.Linq;
using ZigmaWeb.Common.ExtensionMethod;

namespace ZigmaWeb.Common.Configuration
{
    public static class AppFileManager
    {
        public static string GetUserProfileImagePath(int userId, ProfileImageSize profileImageSize, string fileExtensionsToSearch = "jpeg,png")
        {
            string path;
            if (TryGetUserProfileImagePath(userId, profileImageSize, out path, fileExtensionsToSearch))
                return path;
            throw new FileNotFoundException();
        }

        public static bool TryGetUserProfileImagePath(int userId, ProfileImageSize profileImageSize, out string userProfileImagePath, string fileExtensionsToSearch = "jpeg,png")
        {
            var extensions = fileExtensionsToSearch.Split(',');
            foreach (var ext in extensions) {
                var path = $"{AppDirectoryManager.GetUserProfileImageDirectory(userId)}\\{profileImageSize.ToString()}.{ext}";
                if (File.Exists(path)) {
                    userProfileImagePath = path;
                    return true;
                }
            }

            userProfileImagePath = null;
            return false;
        }

        public static string CalculateUserProfileImagePath(int userId, ProfileImageSize profileImageSize, string fileExtension)
        {
            return $"{AppConfigurationManager.AppDirectory}\\Users\\{userId}\\ProfileImage\\{profileImageSize.ToString()}{fileExtension}";
        }

        public static void RemoveUserProfileImagesExceptOriginal(int userId)
        {
            var profileImages = Directory.GetFiles(AppDirectoryManager.GetUserProfileImageDirectory(userId));
            profileImages.ForEach(image => File.Delete(image));
        }

        public static void RemoveUserProfileImage(int userId, ProfileImageSize profileImageSize)
        {
            var image = GetUserProfileImagePath(userId, profileImageSize);
            if (File.Exists(image))
                File.Delete(image);
        }

        public static void RemoveUserProfileImages(int userId, params ProfileImageSize[] profileImageSizes)
        {
            profileImageSizes.ForEach(profileImageSize => {
                string imagePath;
                if (TryGetUserProfileImagePath(userId, profileImageSize, out imagePath, "jpeg"))
                    File.Delete(imagePath);

                if (TryGetUserProfileImagePath(userId, profileImageSize, out imagePath, "png"))
                    File.Delete(imagePath);
            });
        }

        public static string GetMediaFilePath(string mediaFileName)
        {
            return $"{AppConfigurationManager.GetUserMediaDirectory()}\\{mediaFileName}";
        }
    }
}
