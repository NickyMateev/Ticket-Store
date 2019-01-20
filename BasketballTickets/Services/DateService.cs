using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballTickets.Services
{
    public class DateService
    {
        public static string GetDayOfWeek(DateTime date)
        {
            return date.DayOfWeek.ToString();
        }

        public static string GetDayOfMonth(DateTime date)
        {
            string monthName = date
                .ToString("MMM", CultureInfo.InvariantCulture);

            return monthName + " " + date.Day;
        }

        public static string GetTimeOfDay(DateTime date)
        {
            return date.ToString("hh:mm tt");
        }

        public static string GetFormattedGameDate(string awayTeam, string homeTeam, DateTime date)
        {
            return awayTeam + " @ " + homeTeam + " (" + GetDayOfMonth(date) + ", " + date.Year + ")";
        }
    }
}
