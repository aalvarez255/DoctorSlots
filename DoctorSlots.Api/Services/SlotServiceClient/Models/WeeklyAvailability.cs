using DoctorSlots.Api.Services.SlotServiceClient.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorSlots.Api.SlotServiceClient.Models
{
    [JsonConverter(typeof(SlotServiceSerializer))]
    public class WeeklyAvailability
    {
        public WeeklyAvailability()
        {
            DaysAvailability = new List<DailyAvailability>();
        }

        public Facility Facility { get; set; }
        public int SlotDurationMinutes { get; set; }      
        public List<DailyAvailability> DaysAvailability { get; set; }
    }
}
