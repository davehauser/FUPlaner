using System;

namespace FUPlaner.Helpers
{
    public static class DateTimeHelper
    {
        public static string GetDayName(this DateTime date)
        {
            var culture = new System.Globalization.CultureInfo("de-DE");
            return culture.DateTimeFormat.GetDayName(date.DayOfWeek);
        }
    }
}