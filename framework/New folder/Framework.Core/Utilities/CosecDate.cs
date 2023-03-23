using System.Globalization;

namespace Framework.Core.Utilities ;
public static class CosecDate
{
    public static string DateFormatToday()
    {
        var today = DateTime.Now.ToPersianString("yyyy/MM/dd");
        PersianCalendar pc = new PersianCalendar();
        int year = int.Parse(today.Substring(0, 4));
        int month = int.Parse(today.Substring(5, 2));
        int day = int.Parse(today.Substring(8, 2));
        return new DateTime(year, month, day, pc).ToString("ddMMyyyy");
    }

    public static DateTime DateFormat(string date)
    {
        PersianCalendar pc = new PersianCalendar();
        int year = int.Parse(date.Substring(0, 4));
        int month = int.Parse(date.Substring(5, 2));
        int day = int.Parse(date.Substring(8, 2));
        DateTime DateFrom = new DateTime(year, month, day, pc);
        return DateFrom;

    }
    public static string DateFormatString(string date)
    {
        PersianCalendar pc = new PersianCalendar();
        int year = int.Parse(date.Substring(0, 4));
        int month = int.Parse(date.Substring(5, 2));
        int day = int.Parse(date.Substring(8, 2));
        DateTime DateFrom = new DateTime(year, month, day, pc);
        return DateFrom.ToPersianString("yyyy/MM/dd");
    }

}