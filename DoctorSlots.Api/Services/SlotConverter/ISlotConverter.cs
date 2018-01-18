using DoctorSlots.Api.DTOs;
using DoctorSlots.Api.SlotServiceClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorSlots.Api.Services.SlotParser
{
    public interface ISlotConverter
    {
        /* Converts a WeeklyAvailability with work periods to a list of slots */ 
        List<Slot> ParseWorkPeriods(WeeklyAvailability weeklyAvailability, DateTime firstDayOfWeek);
    }
}
