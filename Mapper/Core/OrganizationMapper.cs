using ZigmaWeb.Mapper.Attributes;
using ZigmaWeb.Mapper.Profile;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.Organization;

namespace ZigmaWeb.Mapper.Core
{
    [ObjectMapper]
    public static class OrganizationMapper
    {
        public static void CreateMap(AutoMapperProfile profile)
        {
            profile.CreateMap<Organization, OrganizationPM>();
            profile.CreateMap<OrganizationPM, Organization>();
        }

        public static Organization GetOrganization(this OrganizationPM organizationPM)
        {
            return AutoMapper.Mapper.Map<OrganizationPM, Organization>(organizationPM);
        }

        public static OrganizationPM GetPresentationModel(this Organization organization)
        {
            return AutoMapper.Mapper.Map<Organization, OrganizationPM>(organization);
        }
    }
}
