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
            List<Slot> slots = new List<Slot>();
            return slots;
        }
    }
}
