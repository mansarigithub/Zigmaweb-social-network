using System;
using System.Collections.Generic;
using System.Linq;
using ZigmaWeb.Common.Data;

namespace ZigmaWeb.Common.DataProvider
{
    public class ChartDataHelper
    {
        public static void FillBlankDays<T>(IList<T> chartData) where T : IChartData, new()
        {
            if (chartData.All(r => r.Date.Date != DateTime.Now.Date))
            {
                chartData.Add(new T { Date = DateTime.Now.Date });
            }

            for (var i = 1; i < chartData.Count; i++)
            {
                var firstDate = chartData[i - 1].Date;
                var secondDate = chartData[i].Date;

                var daysBetween = (secondDate - firstDate).TotalDays - 1;

                if (daysBetween > 0)
                {
                    for (var j = 0; j < daysBetween; j++)
                    {
                        chartData.Insert(i + j, new T {Date = firstDate.AddDays(j + 1)});
                    }
                }
            }
        }
    }
}
