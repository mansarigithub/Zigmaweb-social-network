using ZigmaWeb.Mapper.Attributes;
using ZigmaWeb.Mapper.Profile;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.PresentationModel.Model.Job;

namespace ZigmaWeb.Mapper.Core
{
    [ObjectMapper]
    public static class JobMapper
    {
        public static void CreateMap(AutoMapperProfile profile)
        {
            profile.CreateMap<Job, JobPM>();
            profile.CreateMap<JobPM, Job>();
        }

        public static Job GetJob(this JobPM jobPresentationModel)
        {
            return AutoMapper.Mapper.Map<JobPM, Job>(jobPresentationModel);
        }

        public static JobPM  GetPresentationModel(this Job job)
        {
            return AutoMapper.Mapper.Map<Job, JobPM>(job);
        }
    }
}
