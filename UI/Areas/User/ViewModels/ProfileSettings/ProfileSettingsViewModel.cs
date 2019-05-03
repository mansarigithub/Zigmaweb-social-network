using System;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.PresentationModel.Model;

namespace ZigmaWeb.UI.Areas.User.ViewModels.Profile
{
    public class ProfileSettingsViewModel
    {
        public ProfileGeneralSettingsPM GeneralSettings { get; set; }
        public ProfileSocialNetworkLinksPM ProfileSocialNetworkUrls { get; set; }
        public ProfileChangePasswordPM ChangePasswordPM { get; set; }
        public ProfileStatisticsAndSocialLinksPM ProfileStatisticsAndSocialLinks { get; set; }
    }
}