using System.Linq;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Mapper.Attributes;
using ZigmaWeb.Mapper.Profile;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.Content;
using ZigmaWeb.Security.ExtensionMethod;

namespace ZigmaWeb.Mapper.Core
{
    [ObjectMapper]
    public static class UserMapper
    {
        public static void CreateMap(AutoMapperProfile profile)
        {
            profile.CreateMap<UserRegistrationPM, User>();

            // UserInfoPm
            profile.CreateMap<User, UserInfoPm>()
                .ForMember(pm => pm.FullName, opt => opt.Ignore())
                .ForMember(pm => pm.IsApproved, opt => opt.MapFrom(model => model.Membership.IsApproved))
                .ForMember(pm => pm.IsLockedOut, opt => opt.MapFrom(model => model.Membership.IsLockedOut));

            profile.CreateMap<User, AdminDashboardNewUserPm>()
                .ForMember(pm => pm.FullName, opt => opt.Ignore())
                .ForMember(pm => pm.ImageUrl, opt => opt.Ignore());


            // UserInfoPm
            profile.CreateMap<User, ProfileGeneralSettingsPM>();
        }

        public static User GetUser(this UserRegistrationPM userRegistrationPM)
        {
            return AutoMapper.Mapper.Map<UserRegistrationPM, User>(userRegistrationPM);
        }

        public static ProfileGeneralSettingsPM GetProfileGeneralSettingsPM(this User user)
        {
            var pm = AutoMapper.Mapper.Map<User, ProfileGeneralSettingsPM>(user);
            pm.AboutMe = user.ProfileKeyValues.SingleOrDefault(kv => kv.Type == ProfileKeyValueType.AboutMe)?.Value;
            pm.MobileNumber = user.ProfileKeyValues.SingleOrDefault(kv => kv.Type == ProfileKeyValueType.MobileNumber)?.Value;
            pm.WebSiteUrl = user.ProfileKeyValues.SingleOrDefault(kv => kv.Type == ProfileKeyValueType.WebSiteUrl)?.Value;
            var sexuality = user.ProfileKeyValues.SingleOrDefault(kv => kv.Type == ProfileKeyValueType.Sexuality)?.Value;
            pm.Sexuality = (sexuality == null ? (Sexuality?)null : (Sexuality)int.Parse(sexuality));
            return pm;
        }
    }
}
