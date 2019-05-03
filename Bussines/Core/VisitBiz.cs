using System;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Bussines.Infrastructure;
using ZigmaWeb.UnitOfWork;
using ZigmaWeb.Common.Enum;
using System.Linq;
using System.Collections.Generic;
using ZigmaWeb.Common.Data;

namespace ZigmaWeb.Bussines.Core
{
    public class VisitBiz : BizBase<Visit>
    {
        public VisitBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {

        }

        public int ReadUserTodayTotalVisits(int userId)
        {
            var today = DateTime.Now.Date;
            return UnitOfWork.VisitRepository
                .Read(visit => visit.Content.AuthorId == userId && visit.Date == today)
                .Sum(visit => (int?)visit.Count) ?? 0;
        }

        public void IncrementContentVisits(int contentId, int incrementValue = 1)
        {
            var today = DateTime.Now.Date;
            var visit = ReadSingleOrDefault(v => v.ContentId == contentId && v.Date == today);
            if (visit == null)
                UnitOfWork.VisitRepository.Add(new Visit() {
                    ContentId = contentId,
                    Date = today,
                    Count = incrementValue,
                });
            else
                visit.Count += incrementValue;
        }

        public List<ChartData> GetContentVisitChartDataForUserDashboard(int userId, ContentType contentType, int days = 7)
        {
            var startDate = DateTime.Now.Date;
            var endDate = startDate.AddDays(-days);
            return Read(v =>
                 v.Content.AuthorId == userId &&
                 v.Date >= endDate &&
                 v.Content.Type == contentType &&
                 v.Content.State == ContentState.Published)
            .GroupBy(visit => visit.Date)
            .Select(group => new ChartData() {
                Date = group.Key,
                Value = group.Sum(visit => visit.Count)
            })
            .ToList();
        }

        public int GetTodaySiteTotalVisitsCount()
        {
            var today = DateTime.Now.Date;
            return Read(v => v.Date == today).Sum(v => (int?)v.Count) ?? 0;
        }
    }
}