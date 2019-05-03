using ZigmaWeb.Mapper.Attributes;
using ZigmaWeb.Mapper.Profile;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.PresentationModel.Model.JobResume;

namespace ZigmaWeb.Mapper.Core
{
    [ObjectMapper]
    public static class JobResumeMapper
    {
        public static void CreateMap(AutoMapperProfile profile)
        {
            profile.CreateMap<JobResume, JobResumePM>();
            profile.CreateMap<JobResumePM, JobResume>();
        }

        public static JobResume GetJobResume(this JobResumePM jobResumePM)
        {
            return AutoMapper.Mapper.Map<JobResumePM, JobResume>(jobResumePM);
        }

        public static JobResumePM  GetPresentationModel(this JobResume jobResume)
        {
            return AutoMapper.Mapper.Map<JobResume, JobResumePM>(jobResume);
        }
    }
}
