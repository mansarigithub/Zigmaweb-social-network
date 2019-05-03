using ZigmaWeb.Common.Globalization;

namespace ZigmaWeb.PresentationModel.Model.Archive
{
    public class ContentArchiveItemPM
    {
        public ContentArchiveItemPM()
        {
        }

        public ContentArchiveItemPM(int year, int month)
        {
            Year = year;
            Month = month;
        }

        public int Month { get; set; }
        public int Year { get; set; }
        public string PersianMonthName
        {
            get
            {
                return PersianCalendarHelper.GetMonthName(Month);
            }

        }
    }
}
