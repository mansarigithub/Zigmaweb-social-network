using System;
using ZigmaWeb.Common.DataProvider;

namespace ZigmaWeb.Common.Data
{
    public class ChartData : IChartData
    {
        public DateTime Date { get; set; }

        public int Value { get; set; }
    }
}
