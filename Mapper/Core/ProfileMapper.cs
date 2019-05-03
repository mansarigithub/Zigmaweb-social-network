using ZigmaWeb.Mapper.Attributes;
using ZigmaWeb.Mapper.Profile;
using ZigmaWeb.PresentationModel.Model;
using DomainModel = ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.Mapper.Core
{
    [ObjectMapper]
    public static class ProfileMapper
    {
        public static void CreateMap(AutoMapperProfile profile)
        {
            profile.CreateMap<DomainModel.ProfileKeyValue, ProfileForViewByVisitorPM>();
            profile.CreateMap<ProfileForViewByVisitorPM, DomainModel.ProfileKeyValue>();


        }

        public static DomainModel.ProfileKeyValue GetProfile(this ProfileForViewByVisitorPM ProfilePresentationModel)
        {
            return AutoMapper.Mapper.Map<ProfileForViewByVisitorPM, DomainModel.ProfileKeyValue>(ProfilePresentationModel);
        }

        public static ProfileForViewByVisitorPM GetPresentationModel(this DomainModel.ProfileKeyValue Profile)
        {
            return AutoMapper.Mapper.Map<DomainModel.ProfileKeyValue, ProfileForViewByVisitorPM>(Profile);
        }
    }
}
