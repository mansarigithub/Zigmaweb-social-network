using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Bussines.Infrastructure;
using ZigmaWeb.UnitOfWork;
using System;
using System.Data.Entity;
using System.Linq;

namespace ZigmaWeb.Bussines.Core
{
    public class JobBiz : BizBase<Job>
    {
        public JobBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {
        }

        public Job AddJobIfNotExist(string jobName)
        {
            jobName = jobName.Trim();
            var job = ReadSingleOrDefault(x => x.Name == jobName);
            if (job != null) return job;
            return Add(new Job() {
                Name = jobName,
            });
        }

        public IQueryable<Job> SuggestJob(string phrase)
        {
            return Read(x => x.Name.Contains(phrase)).Take(5);
        }
    }
}