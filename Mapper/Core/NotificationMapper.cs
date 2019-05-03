using ZigmaWeb.Common.Enum;
using ZigmaWeb.Mapper.Attributes;
using ZigmaWeb.Mapper.Profile;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.Common.Globalization;

namespace ZigmaWeb.Mapper.Core
{
    [ObjectMapper]
    public static class NotificationMapper
    {
        public static void CreateMap(AutoMapperProfile profile)
        {
            profile.CreateMap<Notification, NotificationPM>()
                  .ForMember(pm => pm.CreateDateString, opt => opt.Ignore());
        }

        public static NotificationPM GetNotificationPM(this Notification notification)
        {
            return AutoMapper.Mapper.Map<Notification, NotificationPM>(notification);
        }
    }
}
