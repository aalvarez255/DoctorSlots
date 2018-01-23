using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorSlots.Api.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime GetMondayOfWeek(this DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        public static bool IsNullOrMin(this DateTime date)
        {
            return date == null || date == DateTime.MinValue;
        }
    }
}
