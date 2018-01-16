using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorSlots.Api.SlotService.Models
{
    public class DailyAvailability
    {
        public DailyAvailability()
        {
            BusySlots = new List<Slot>();
        }

        public string Day { get; set; }

        public WorkPeriod WorkPeriod { get; set; }
        public List<Slot> BusySlots { get; set; }
    }
}
