using System;
using System.Globalization;

namespace ZigmaWeb.Common.Globalization
{
    public class PersianCalendarHelper
    {
        public static string GetMonthName(int month)
        {
            string monthName;
            switch (month) {
                case 1:
                    monthName = "فروردین";
                    break;
                case 2:
                    monthName = "اردیبهشت";
                    break;
                case 3:
                    monthName = "خرداد";
                    break;
                case 4:
                    monthName = "تیر";
                    break;
                case 5:
                    monthName = "مرداد";
                    break;
                case 6:
                    monthName = "شهریور";
                    break;
                case 7:
                    monthName = "مهر";
                    break;
                case 8:
                    monthName = "آبان";
                    break;
                case 9:
                    monthName = "آذر";
                    break;
                case 10:
                    monthName = "دی";
                    break;
                case 11:
                    monthName = "بهمن";
                    break;
                case 12:
                    monthName = "اسفند";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(monthName));
            }
            return monthName;
        }

        public static string GetNumericPersianDate(DateTime d, string sperator = "/")
        {
            string persianDate = "";
            PersianCalendar pc = new PersianCalendar();

            persianDate = string.Format("{0}/{1}/{2}",
                pc.GetYear(d),
                pc.GetMonth(d),
                pc.GetDayOfMonth(d));
            return persianDate;
        }
        //#region Calander
        ///// <summary>
        ///// Convert English_DateTime to Solar_DateTime
        ///// </summary>
        ///// <param name="dateTime">Date Time</param>
        ///// <returns>Solar_DateTime</returns>
        //public static string ConvertToSolarDate(DateTime dateTime)
        //{
        //    string strDateTime = string.Empty;

        //    PersianCalendar pc = new PersianCalendar();
        //    int year = pc.GetYear(dateTime);
        //    int month = pc.GetMonth(dateTime);
        //    int day = pc.GetDayOfMonth(dateTime);

        //    strDateTime = String.Format("{1:0000}{0}{2:00}{0}{3:00}",
        //        System.Globalization.DateTimeFormatInfo.CurrentInfo.DateSeparator,
        //        year, month, day);

        //    return strDateTime;
        //}

        ///// <summary>
        ///// Convert English_DateTime String to Solar_DateTime 
        ///// </summary>
        ///// <param name="dateTime">Date Time</param>
        ///// <returns>Solar_DateTime</returns>
        //public static string ConvertToSolarDate(string dateTime)
        //{
        //    try
        //    {
        //        return ConvertToSolarDate(Convert.ToDateTime(dateTime));
        //    }
        //    catch
        //    {
        //        return string.Empty;
        //    }
        //}

        ///// <summary>
        ///// Convert English_DateTime String to Solar_DateTime 
        ///// </summary>
        ///// <param name="dateTime">Date Time</param>
        ///// <returns>Solar_DateTime</returns>
        //public static string ConvertToSolarDate(object dateTime)
        //{
        //    return ConvertToSolarDate(dateTime.ToString());
        //}

        ///// <summary>
        ///// Return DateTime string with Month
        ///// </summary>
        ///// <param name="textdate"></param>
        ///// <param name="WithPesianMonth"></param>
        ///// <returns></returns>
        //public static string ConvertToSolarDate(string textdate, bool WithPesianMonth)
        //{
        //    try
        //    {
        //        DateTime dt = Convert.ToDateTime(textdate);

        //        string shamsiDate = string.Empty;

        //        PersianCalendar pc = new PersianCalendar();

        //        int year = pc.GetYear(dt);

        //        int month = pc.GetMonth(dt);

        //        int day = pc.GetDayOfMonth(dt);

        //        //shamsiDate = String.Format("{1:0000}{0}{2:00}{0}{3:00}",DateTimeFormatInfo.CurrentInfo.DateSeparator,year, month, day);

        //        if (WithPesianMonth)
        //            return day.ToString() + " " + GetPersianMonthName(month) + " " + year.ToString();
        //        else
        //            return year.ToString() + "/" + month.ToString() + "/" + day.ToString();
        //    }
        //    catch
        //    {
        //        return string.Empty;
        //    }
        //}

        ///// <summary>
        ///// Return DateTime string with Month
        ///// </summary>
        ///// <param name="textdate"></param>
        ///// <param name="WithPesianMonth"></param>
        ///// <returns></returns>
        //public static string ConvertToSolarDate(object date, bool WithPesianMonth)
        //{
        //    return ConvertToSolarDate(date.ToString(), WithPesianMonth);
        //}

        ///// <summary>
        ///// Convert  Solar_DateTime to English_DateTime
        ///// </summary>
        ///// <param name="dateTime">Date Time</param>
        ///// <returns>Solar_DateTime</returns>
        //public static DateTime ConvertToEngilshDate(string dateTime)
        //{
        //    string[] dateparts = dateTime.Split('/');

        //    PersianCalendar pc = new PersianCalendar();

        //    DateTime dt = new DateTime(Convert.ToInt32(dateparts[0]), Convert.ToInt32(dateparts[1]), Convert.ToInt32(dateparts[2]), pc);
        //    return dt;
        //}

        ///// <summary>
        ///// Get month name
        ///// </summary>
        ///// <param name="month">month number</param>
        ///// <returns>month name</returns>
        //public static string GetPersianMonthName(int month)
        //{
        //    switch (month)
        //    {
        //        case 1:
        //            return "فروردین";
        //        case 2:
        //            return "اردیبهشت";
        //        case 3:
        //            return "خرداد";
        //        case 4:
        //            return "تیر";
        //        case 5:
        //            return "مرداد";
        //        case 6:
        //            return "شهریور";
        //        case 7:
        //            return "مهر";
        //        case 8:
        //            return "آبان";
        //        case 9:
        //            return "آذر";
        //        case 10:
        //            return "دی";
        //        case 11:
        //            return "بهمن";
        //        case 12:
        //            return "اسفند";
        //        default:
        //            return month.ToString();
        //    }
        //}

        //#region Extensions

        ///// <summary>
        ///// Convert To Solar Date
        ///// </summary>
        ///// <param name="dateTime"></param>
        ///// <returns></returns>
        //public static string ToSolarDate(this string dateTime)
        //{
        //    return ConvertToSolarDate(dateTime);
        //}

        ///// <summary>
        ///// Convert To Solar Date
        ///// </summary>
        ///// <param name="dateTime"></param>
        ///// <returns></returns>
        //public static string ToSolarDate(this object dateTime)
        //{
        //    return ConvertToSolarDate(dateTime);
        //}

        ///// <summary>
        ///// Convert To Solar Date
        ///// </summary>
        ///// <param name="dateTime"></param>
        ///// <returns></returns>
        //public static string ToSolarDate(this object dateTime, bool withMonthName)
        //{
        //    return ConvertToSolarDate(dateTime, withMonthName);
        //}

        //#endregion

        //#endregion


        //public static string EvalAgeByBirthdate(DateTime? birthday)
        //{
        //    if (!birthday.HasValue)
        //        return "-";

        //    DateTime today = DateTime.Today;
        //    int age = today.Year - birthday.Value.Year;

        //    if (birthday > today.AddYears(-age))
        //        age--;

        //    return age.ToString();
        //}

    }
}
