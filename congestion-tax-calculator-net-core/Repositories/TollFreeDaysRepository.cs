using System;
using System.Collections.Generic;
using System.Text;

namespace congestion.calculator.Repositories
{
    public class TollFreeDaysRepository
    {
        public List<DateTime> GetTollFreeDaysList(DateTime startDate, DateTime endDate)
        {
            var exceptionDaysList = new List<DateTime>();

            var holidaysList = GetHolidays(startDate, endDate);
            var weekendDays = GetWeekendDays();
            var exceptionalMonths = GetExceptionalMonths(startDate, endDate);

            for (DateTime currentDate = startDate; currentDate.Date <= endDate; currentDate = currentDate.AddDays(1))
            {
                if (weekendDays.Contains(currentDate.DayOfWeek) ||
                    exceptionalMonths.Contains(currentDate.Month) ||
                    holidaysList.Contains(currentDate) ||
                    holidaysList.Contains(currentDate.AddDays(1))
                    )
                {
                    exceptionDaysList.Add(currentDate);
                }
            }
            return exceptionDaysList;
        }

        private List<DateTime> GetHolidays(DateTime startDate, DateTime endDate)
        {
            var holidays = new List<DateTime>()
            {
                DateTime.Parse("2013-03-26"),
            };
            return holidays;
        }

        private List<DayOfWeek> GetWeekendDays()
        {
            return new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday };
        }

        private List<int> GetExceptionalMonths(DateTime startDate, DateTime endDate)
        {
            return new List<int>() { 7 };
        }
    }
}
