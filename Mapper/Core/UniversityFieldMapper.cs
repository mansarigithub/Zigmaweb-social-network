using ZigmaWeb.Mapper.Attributes;
using ZigmaWeb.Mapper.Profile;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.PresentationModel.Model.UniversityField;

namespace ZigmaWeb.Mapper.Core
{
    [ObjectMapper]
    public static class UniversityFieldMapper
    {
        public static void CreateMap(AutoMapperProfile profile)
        {
            profile.CreateMap<UniversityField, UniversityFieldPM>();
            profile.CreateMap<UniversityFieldPM, UniversityField>();
        }

        public static UniversityField GetUniversityField(this UniversityFieldPM universityFieldPM)
        {
            return AutoMapper.Mapper.Map<UniversityFieldPM, UniversityField>(universityFieldPM);
        }

        public static UniversityFieldPM GetPresentationModel(this UniversityField universityField)
        {
            return AutoMapper.Mapper.Map<UniversityField, UniversityFieldPM>(universityField);
        }
    }
}
