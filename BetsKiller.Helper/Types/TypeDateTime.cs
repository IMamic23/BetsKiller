using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
