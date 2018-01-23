using DoctorSlots.Api.Services.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorSlots.Api.Models
{
    [JsonConverter(typeof(SlotServiceSerializer))]
    public class WeeklyAvailability
    {
        public WeeklyAvailability()
        {
            DaysAvailability = new List<DailyAvailability>();
        }

        public string FacilityId { get; set; }
        public int SlotDurationMinutes { get; set; }      
        public List<DailyAvailability> DaysAvailability { get; set; }
    }
}
