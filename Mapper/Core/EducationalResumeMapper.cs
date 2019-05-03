using ZigmaWeb.Common.Enum;
using ZigmaWeb.Localization.ExtensionMethod;
using ZigmaWeb.Mapper.Attributes;
using ZigmaWeb.Mapper.Profile;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.Organization;

namespace ZigmaWeb.Mapper.Core
{
    [ObjectMapper]
    public static class EducationalResumeMapper
    {
        public static void CreateMap(AutoMapperProfile profile)
        {
            profile.CreateMap<EducationalResume, EducationalResumePM>();
            profile.CreateMap<EducationalResumePM, EducationalResume>();
        }

        public static EducationalResume GetEducationalResume(this EducationalResumePM educationalResumePM)
        {
            return AutoMapper.Mapper.Map<EducationalResumePM, EducationalResume>(educationalResumePM);
        }

        public static EducationalResumePM GetPresentationModel(this EducationalResume educationalResume)
        {
            return AutoMapper.Mapper.Map<EducationalResume, EducationalResumePM>(educationalResume);
        }
    }
}
