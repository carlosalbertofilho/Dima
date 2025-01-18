namespace Dima.Core.Common.Extensions;

public static class DateTimeExtension
{
    public static DateTime GetFirstDayOfMonth(this DateTime date, int? month = null, int? year = null)
    => new(year ?? date.Year, month ?? date.Month, 1);
    
    public static DateTime GetLastDayOfMonth(this DateTime date, int? month = null, int? year = null)
    => new(year ?? date.Year, month ?? date.Month, DateTime.DaysInMonth(year ?? date.Year, month ?? date.Month));
}