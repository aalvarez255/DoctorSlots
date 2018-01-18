﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorSlots.Api.SlotServiceClient.Models
{
    public class DailyAvailability
    {
        public DailyAvailability()
        {
            BusySlots = new List<Slot>();
        }

        public int DayOfWeek { get; set; }  //Sunday is 0, Monday is 1, and so on.

        public WorkPeriod WorkPeriod { get; set; }
        public List<Slot> BusySlots { get; set; }
    }
}