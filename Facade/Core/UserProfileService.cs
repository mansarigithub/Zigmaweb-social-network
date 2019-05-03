using ZigmaWeb.Bussines.Core;
using System.Linq;
using System.Data.Entity;
using ZigmaWeb.Security.Identity;
using ZigmaWeb.UnitOfWork;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.Mapper.Core;
using ZigmaWeb.Model.Domain.Core;
using System.Collections.Generic;
using ZigmaWeb.Common.Enum;
using System.IO;
using Common.Exception;
using ZigmaWeb.Localization.ExtensionMethod;
using System.Drawing;
using ZigmaWeb.Common.Configuration;
using ZigmaWeb.Common.Drawing;
using System.Drawing.Imaging;
using ZigmaWeb.Common.IO;
using ZigmaWeb.PresentationModel.ModelContainer;
using ZigmaWeb.Common.ExtensionMethod;
using ZigmaWeb.PresentationModel.Model.Organization;
using ZigmaWeb.Common.Globalization;
using System;

namespace ZigmaWeb.Facade.Core
{
    public class UserProfileService
    {
        CoreUnitOfWork UnitOfWork { get; set; }
        UserBiz UserBiz { get; set; }
        MembershipBiz MembershipBiz { get; set; }
        ProfileBiz ProfileBiz { get; set; }
        ArticleBiz ArticleBiz { get; set; }
        BlogBiz BlogBiz { get; set; }
        VisitBiz VisitBiz { get; set; }
        JobResumeBiz JobResumeBiz { get; set; }
        EducationalResumeBiz EducationalResumeBiz { get; set; }
        FollowBiz FollowBiz { get; set; }

        public UserProfileService()
        {
            UnitOfWork = new CoreUnitOfWork();
            UserBiz = new UserBiz(UnitOfWork);
            MembershipBiz = new MembershipBiz(UnitOfWork);
            ProfileBiz = new ProfileBiz(UnitOfWork);
            ArticleBiz = new ArticleBiz(UnitOfWork);
            BlogBiz = new BlogBiz(UnitOfWork);
            EducationalResumeBiz = new EducationalResumeBiz(UnitOfWork);
            VisitBiz = new VisitBiz(UnitOfWork);
            JobResumeBiz = new JobResumeBiz(UnitOfWork);
            FollowBiz = new FollowBiz(UnitOfWork);
        }

        public ProfileStatisticsAndSocialLinksPM ReadProfileStatisticsAndSocialLinks(int userId)
        {
            var userLinks = ProfileBiz.ReadUserProfileValues(userId,
                ProfileKeyValueType.WebSiteUrl,
                ProfileKeyValueType.FacebookUrl,
                ProfileKeyValueType.TwitterUrl,
                ProfileKeyValueType.LinkedInUrl);
            var articlesCount = ArticleBiz.ReadUserPublishedContents(userId, ContentType.Article).Count();
            var blogsCount = ArticleBiz.ReadUserPublishedContents(userId, ContentType.Article).Count();

            return new ProfileStatisticsAndSocialLinksPM() {
                WebSiteUrl = userLinks.SingleOrDefault(kv => kv.Type == ProfileKeyValueType.WebSiteUrl)?.Value,
                FacebookUrl = userLinks.SingleOrDefault(kv => kv.Type == ProfileKeyValueType.FacebookUrl)?.Value,
                TwitterUrl = userLinks.SingleOrDefault(kv => kv.Type == ProfileKeyValueType.TwitterUrl)?.Value,
                LinkedInUrl = userLinks.SingleOrDefault(kv => kv.Type == ProfileKeyValueType.LinkedInUrl)?.Value,
                TotalArticles = ArticleBiz.ReadUserPublishedContents(userId, ContentType.Article).Count(),
                TotalBlogPosts = ArticleBiz.ReadUserPublishedContents(userId, ContentType.BlogPost).Count(),
                TotalVisits = VisitBiz.ReadUserTodayTotalVisits(userId),
            };
        }


        #region General Settings
        public ProfileGeneralSettingsPM ReadProfileGeneralSettings(int userId)
        {
            return UserBiz.Read(user => user.Id == userId)
                .Include(user => user.ProfileKeyValues)
                .Single()
                .GetProfileGeneralSettingsPM();
        }

        public void UpdateUserGeneralSettings(UserIdentity userIdentity, ProfileGeneralSettingsPM generalSettings)
        {
            // Update User
            var user = UserBiz.Find(userIdentity.UserId);
            user.FirstName = generalSettings.FirstName;
            user.LastName = generalSettings.LastName;

            // Update Profile
            var profileKeyValues = new List<ProfileKeyValue>();
            profileKeyValues.Add(new ProfileKeyValue() {
                Type = ProfileKeyValueType.MobileNumber,
                Value = string.IsNullOrWhiteSpace(generalSettings.MobileNumber) ? "" : generalSettings.MobileNumber.Trim()
            });
            profileKeyValues.Add(new ProfileKeyValue() {
                Type = ProfileKeyValueType.WebSiteUrl,
                Value = string.IsNullOrWhiteSpace(generalSettings.WebSiteUrl) ? "" : generalSettings.WebSiteUrl.Trim()
            });
            profileKeyValues.Add(new ProfileKeyValue() {
                Type = ProfileKeyValueType.AboutMe,
                Value = string.IsNullOrWhiteSpace(generalSettings.AboutMe) ? "" : generalSettings.AboutMe.Trim()
            });

            profileKeyValues.Add(new ProfileKeyValue() {
                Type = ProfileKeyValueType.Sexuality,
                Value = generalSettings.Sexuality.HasValue ? ((int)generalSettings.Sexuality).ToString() : null
            });

            ProfileBiz.SetUserProfile(userIdentity.UserId, profileKeyValues);
            UnitOfWork.SaveChanges();

            userIdentity.FirstName = user.FirstName;
            userIdentity.LastName = user.LastName;
        }

        #endregion

        #region Social Network Urls
        public void UpdateUserSocialNetworkUrls(UserIdentity userIdentity, ProfileSocialNetworkLinksPM profileSocialNetworkLinksPM)
        {
            var linkedInUrl = string.IsNullOrWhiteSpace(profileSocialNetworkLinksPM.LinkedInUrl) ? "" : profileSocialNetworkLinksPM.LinkedInUrl.Trim();
            var facebookUrl = string.IsNullOrWhiteSpace(profileSocialNetworkLinksPM.FacebookUrl) ? "" : profileSocialNetworkLinksPM.FacebookUrl.Trim();
            var twitterUrl = string.IsNullOrWhiteSpace(profileSocialNetworkLinksPM.TwitterUrl) ? "" : profileSocialNetworkLinksPM.TwitterUrl.Trim();

            var profileKeyValues = new List<ProfileKeyValue>();
            profileKeyValues.Add(new ProfileKeyValue(ProfileKeyValueType.LinkedInUrl, linkedInUrl));
            profileKeyValues.Add(new ProfileKeyValue(ProfileKeyValueType.FacebookUrl, facebookUrl));
            profileKeyValues.Add(new ProfileKeyValue(ProfileKeyValueType.TwitterUrl, twitterUrl));

            ProfileBiz.SetUserProfile(userIdentity.UserId, profileKeyValues);
            UnitOfWork.SaveChanges();
        }

        #endregion

        #region Security Settings
        public void UpdateUserPassword(int userId, string currentPassword, string newPassword)
        {
            MembershipBiz.UpdateUserPassword(userId, currentPassword, newPassword);
            UnitOfWork.SaveChanges();
        }

        #endregion

        #region Profile Image
        public void SaveUserOriginalProfileImage(UserIdentity user, Stream inputStream, out int imageWidth, out int imageHeight)
        {
            AppDirectoryManager.CreateUserProfileImageDirectory(user.UserId);
            using (var image = ValidateFileAndGenerateImage(inputStream)) {
                string previousOriginalImagePath;
                var imageFormat = ImageHelper.GetImageFormat(image);
                var extension = ImageHelper.GetImageFormatFileExtension(imageFormat);
                var newOriginalImagePath = AppFileManager.CalculateUserProfileImagePath(user.UserId, ProfileImageSize.Original, extension);
                if (AppFileManager.TryGetUserProfileImagePath(user.UserId, ProfileImageSize.Original, out previousOriginalImagePath))
                    if (previousOriginalImagePath != newOriginalImagePath)
                        File.Delete(previousOriginalImagePath);
                image.Save(newOriginalImagePath, imageFormat);
                imageWidth = image.Width;
                imageHeight = image.Height;
            }
        }

        private Image ValidateFileAndGenerateImage(Stream inputStream)
        {
            var fileSize = (int)inputStream.Length;
            Image image = null;

            //if (fileSize > 300 * 1024)
            //    throw new BusinessException("ProfileImageSizeIsTooLarge".Localize());
            try {
                image = Image.FromStream(inputStream);
            }
            catch {
                throw new BusinessException("UploadedFileIsNotvalidImage".Localize());
            }
            //if (image.Width > 800 || image.Height > 800)
            //    throw new BusinessException("ProfileImageDimensionsIsTooLarge".Localize());
            if (image.Width < 100 || image.Height < 100)
                throw new BusinessException("ProfileImageDimensionsIsTooSmall".Localize());

            var imageFormat = ImageHelper.GetImageFormat(image);
            if (!(imageFormat == ImageFormat.Jpeg || imageFormat == ImageFormat.Png))
                throw new BusinessException("ProfileImageTypeIsNotSupported".Localize());

            return image;
        }

        public void CropUserProfileImages(int userId, ProfileImageCropInfoPM cropInfo)
        {
            var originalImagePath = AppFileManager.GetUserProfileImagePath(userId, ProfileImageSize.Original);
            var extension = Path.GetExtension(originalImagePath);
            var largeImagePath = AppFileManager.CalculateUserProfileImagePath(userId, ProfileImageSize.Large, extension);
            var mediumImagePath = AppFileManager.CalculateUserProfileImagePath(userId, ProfileImageSize.Medium, extension);
            var smallImagePath = AppFileManager.CalculateUserProfileImagePath(userId, ProfileImageSize.Small, extension);
            var sourceImageCropArea = cropInfo.GetSourceImageCropArea();

            AppFileManager.RemoveUserProfileImages(userId, ProfileImageSize.Large, ProfileImageSize.Medium, ProfileImageSize.Small);
            ImageHelper.CropImage(originalImagePath, sourceImageCropArea, new Size(300, 300), largeImagePath);
            ImageHelper.CropImage(originalImagePath, sourceImageCropArea, new Size(80, 80), mediumImagePath);
            ImageHelper.CropImage(originalImagePath, sourceImageCropArea, new Size(40, 40), smallImagePath);
            File.Delete(originalImagePath);
            ProfileBiz.SetUserProfile(userId, ProfileKeyValueType.IsProfileImageSet, "1");
            UnitOfWork.SaveChanges();
        }

        public void GetOriginalProfileImageFilePath(int userId, out string path, out string fileMime)
        {
            path = AppFileManager.GetUserProfileImagePath(userId, ProfileImageSize.Original);
            fileMime = MimeHelper.GetMime(Path.GetExtension(path));
        }

        public bool TryGetProfileImageFilePath(int userId, ProfileImageSize profileImageSize, out string path, out string fileMime)
        {
            var userHasProfileImage = HasUserProfileImage(userId);
            if (userHasProfileImage) {
                path = AppFileManager.GetUserProfileImagePath(userId, profileImageSize);
                fileMime = MimeHelper.GetMime(Path.GetExtension(path));
                return true;
            }

            path = null;
            fileMime = null;
            return false;
        }

        public bool HasUserProfileImage(int userId)
        {
            string value;
            var keyFound = ProfileBiz.TryReadUserProfileValue(userId, ProfileKeyValueType.IsProfileImageSet, out value);
            return keyFound && value == "1";
        }

        #endregion

        public UserInfoForHomePageModelContainer ReadUserInfoForProfilePage(int userId, UserIdentity userIdentity)
        {
            var user = UserBiz.Read(u => u.Id == userId)
                .Include(u => u.Membership)
                .Include(u => u.EducationalResumes.Select(x => x.Organization))
                .Include(u => u.EducationalResumes.Select(x => x.UniversityField))
                .Include(u => u.JobResumes.Select(x => x.Organization))
                .Include(u => u.JobResumes.Select(x => x.Job))
                .Single();
            var profileKeyValues = ProfileBiz.ReadUserProfileValues(userId,
                ProfileKeyValueType.WebSiteUrl,
                ProfileKeyValueType.FacebookUrl,
                ProfileKeyValueType.TwitterUrl,
                ProfileKeyValueType.LinkedInUrl,
                ProfileKeyValueType.AboutMe);
            var userBlog = BlogBiz.ReadSingleOrDefault(b => b.CreatorId == userId);

            return new UserInfoForHomePageModelContainer() {
                UserId = userId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AboutMe = profileKeyValues.SingleOrDefault(kv => kv.Type == ProfileKeyValueType.AboutMe)?.Value,
                WebSiteUrl = profileKeyValues.SingleOrDefault(kv => kv.Type == ProfileKeyValueType.WebSiteUrl)?.Value,
                FacebookUrl = profileKeyValues.SingleOrDefault(kv => kv.Type == ProfileKeyValueType.FacebookUrl)?.Value,
                TwitterUrl = profileKeyValues.SingleOrDefault(kv => kv.Type == ProfileKeyValueType.TwitterUrl)?.Value,
                LinkedInUrl = profileKeyValues.SingleOrDefault(kv => kv.Type == ProfileKeyValueType.LinkedInUrl)?.Value,
                TotalArticles = ArticleBiz.ReadUserPublishedContents(userId, ContentType.Article).Count(),
                TotalBlogPosts = ArticleBiz.ReadUserPublishedContents(userId, ContentType.BlogPost).Count(),
                TotalVisits = VisitBiz.ReadUserTodayTotalVisits(userId),
                LatestArticles = ArticleBiz.ReadUserLatestPublishedArticles(userId, 200).MapTo<ContentInfo2PM>().ToList(),
                LatestBlogPosts = BlogBiz.ReadUserLatestPosts(userId, 5).MapTo<ContentInfo2PM>().ToList(),
                EducationalResumes = user.EducationalResumes.OrderByDescending(x => x.EducationGrade).Select(x => x.GetPresentationModel()).ToList(),
                JobResumes = user.JobResumes.OrderByDescending(x => x.StartYear).Select(x => x.GetPresentationModel()).ToList(),
                WeblogName = userBlog?.Name,
                RegistrationDateString = user.Membership.CreateDate.ToPersianDate(),
                CurrentUserFollowsThisUser = userIdentity != null ? FollowBiz.AnyFollow(userIdentity.UserId, userId) : false,
                Followers =  FollowBiz.ReadFollowersCount(userId)

            };
        }

        public void FollowUser(int followedUserId, UserIdentity follower)
        {
            if (FollowBiz.Any(f => f.FollowedId == followedUserId && f.FollowerId == follower.UserId))
                return;
            FollowBiz.Add(new Follow() {
                CreateDate = DateTime.Now,
                FollowedId = followedUserId,
                FollowerId = follower.UserId
            });
            UnitOfWork.SaveChanges();
        }

        public void UnfollowUser(int followedUserId, UserIdentity follower)
        {
            FollowBiz.Remove(FollowBiz.ReadSingle(f => f.FollowedId == followedUserId && f.FollowerId == follower.UserId));
            UnitOfWork.SaveChanges();
        }
    }
}
