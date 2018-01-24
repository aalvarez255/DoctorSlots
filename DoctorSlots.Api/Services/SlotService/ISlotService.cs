using DoctorSlots.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorSlots.Api.Services
{
    public interface ISlotService
    {
        /* Gets the WeeklyAvailability (with work periods) for a given date */
        Task<WeeklyAvailability> GetWeeklyAvailability(DateTime date);

        /* Converts a WeeklyAvailability with work periods to a list of slots */
        List<Slot> ParseWorkPeriods(WeeklyAvailability weeklyAvailability, DateTime firstDayOfWeek);

        /* Performs a slot reservation for a given TakeSlot */
        Task PerformSlotReservation(TakeSlot takeSlot);
    }
}
