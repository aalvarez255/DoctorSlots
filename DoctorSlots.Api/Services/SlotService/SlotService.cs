using DoctorSlots.Api.Extensions;
using DoctorSlots.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorSlots.Api.Services
{
    public class SlotService : ISlotService
    {
        private const string GetWeeklyAvailabilityUrl = "availability/GetWeeklyAvailability/{0}";
        private const string TakeSlotUrl = "availability/TakeSlot";

        private readonly IAuthHttpClient _httpClient;

        public SlotService(IAuthHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeeklyAvailability> GetWeeklyAvailability(DateTime date)
        {
            DateTime mondayOfWeek = GetMondayOfWeek(date);

            string requestUrl = string.Format(GetWeeklyAvailabilityUrl, 
                                    mondayOfWeek.ToString("yyyyMMdd"));

            var result = await _httpClient.GetAsync<WeeklyAvailability>(requestUrl);

            if (result == null)
                throw new Exception("Error downloading weekly availability from slots service");

            return result;
        }

        public List<Slot> ParseWorkPeriods(WeeklyAvailability weeklyAvailability, DateTime date)
        {
            DateTime mondayOfWeek = GetMondayOfWeek(date);

            int slotDuration = weeklyAvailability.SlotDurationMinutes;
            List<Slot> slots = new List<Slot>();

            //discard past days
            var todayWeekNumber = (int)(date.DayOfWeek + 6) % 7;
            var validDays = weeklyAvailability.DaysAvailability.Where(d => d.DayOfWeek >= todayWeekNumber);
            foreach (var dailyAvailability in validDays)
            {
                DateTime dayDate = mondayOfWeek.AddDays(dailyAvailability.DayOfWeek);

                TimeSpan startMorningTime = new TimeSpan(dailyAvailability.WorkPeriod.StartHour, 0, 0);
                TimeSpan endMorningTime = new TimeSpan(dailyAvailability.WorkPeriod.LunchStartHour, 0, 0);
                TimeSpan startAfternoonTime = new TimeSpan(dailyAvailability.WorkPeriod.LunchEndHour, 0, 0);
                TimeSpan endAfternoonTime = new TimeSpan(dailyAvailability.WorkPeriod.EndHour, 0, 0);

                List<Slot> morningSlots = GetSlotsBetweenDates(dayDate.Date + startMorningTime, dayDate.Date + endMorningTime, slotDuration);
                List<Slot> afternoonSlots = GetSlotsBetweenDates(dayDate.Date + startAfternoonTime, dayDate.Date + endAfternoonTime, slotDuration);
                List<Slot> daySlots = morningSlots.Concat(afternoonSlots).ToList();

                //remove busy slots from daily result set
                if (dailyAvailability.BusySlots != null && dailyAvailability.BusySlots.Any())
                    daySlots.RemoveAll(s => dailyAvailability.BusySlots.Any(b => b.Start == s.Start));

                //remove past hours
                daySlots.RemoveAll(s => s.Start < date);

                slots.AddRange(daySlots);
            }

            return slots;
        }

        public async Task PerformSlotReservation(TakeSlot takeSlot)
        {
            if (takeSlot.Start.IsNullOrMin() ||
                  takeSlot.End.IsNullOrMin())
                throw new Exception("Start or End date missing");

            if (takeSlot.Start >= takeSlot.End)
                throw new Exception("Start date can't be greater than End date");

            await _httpClient.PostAsync(TakeSlotUrl, takeSlot);
        }

        private DateTime GetMondayOfWeek(DateTime date)
        {
            return date.GetMondayOfWeek();
        }
        private List<Slot> GetSlotsBetweenDates(DateTime start, DateTime end, int duration)
        {
            int hours = end.Hour - start.Hour;
            
            int periods = (hours * 60) / duration;

            return Enumerable.Range(0, periods)
                .Select(p => new Slot()
                {
                    Start = start.AddMinutes(p * duration),
                    End = start.AddMinutes(p * duration + duration)
                })
                .ToList();
        }
    }
}
