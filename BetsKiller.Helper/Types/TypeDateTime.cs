using System;
using System.Globalization;

namespace BetsKiller.Helper.Types
{
    public class TypeDateTime
    {
        /// <summary>
        /// Returns the first day of the week that the specified date is in. 
        /// </summary>
        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            DayOfWeek firstDay = CultureInfo.InvariantCulture.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            // FIX: First day of week in InvarianCulture is Sunday, but we want it to be Monday
            firstDayInWeek = firstDayInWeek.AddDays(1);

            return firstDayInWeek;
        }

        /// <summary>
        /// Returns datetime string in specific format.
        /// </summary>
        public static string ParseDateTime(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return dateTime.Value.ToString("MMM d, yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
