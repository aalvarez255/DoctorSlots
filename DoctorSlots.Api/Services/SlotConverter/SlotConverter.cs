using DoctorSlots.Api.DTOs;
using DoctorSlots.Api.SlotServiceClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorSlots.Api.Services.SlotParser
{
    public class SlotConverter : ISlotConverter
    {
        public List<Slot> ParseWorkPeriods(WeeklyAvailability weeklyAvailability, DateTime firstDayOfWeek)
        {
            int slotDuration = weeklyAvailability.SlotDurationMinutes;
            List<Slot> slots = new List<Slot>();

            foreach (var dailyAvailability in weeklyAvailability.DaysAvailability)
            {
                DateTime dayDate = firstDayOfWeek.AddDays(dailyAvailability.DayOfWeek);

                TimeSpan startMorningTime = new TimeSpan(dailyAvailability.WorkPeriod.StartHour, 0, 0);
                TimeSpan endMorningTime = new TimeSpan(dailyAvailability.WorkPeriod.LunchStartHour, 0, 0);
                TimeSpan startAfternoonTime = new TimeSpan(dailyAvailability.WorkPeriod.LunchEndHour, 0, 0);
                TimeSpan endAfternoonTime = new TimeSpan(dailyAvailability.WorkPeriod.EndHour, 0, 0);

                List<Slot> morningSlots = GetSlotsBetweenDates(dayDate.Date + startMorningTime, dayDate.Date + endMorningTime, slotDuration);
                List<Slot> afternoonSlots = GetSlotsBetweenDates(dayDate.Date + startAfternoonTime, dayDate.Date + endAfternoonTime, slotDuration);
                List<Slot> daySlots = morningSlots.Concat(afternoonSlots).ToList();

                //remove busy slots from daily result set
                if (dailyAvailability.BusySlots != null && dailyAvailability.BusySlots.Any())
                    daySlots.RemoveAll(s => dailyAvailability.BusySlots.Contains(s));

                slots.AddRange(daySlots);
            }

            return slots;
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
