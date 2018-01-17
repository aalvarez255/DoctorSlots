using DoctorSlots.Api.Services.SlotService.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorSlots.Api.SlotService.Models
{
    [JsonConverter(typeof(SlotJsonConverter))]
    public class WeeklyAvailability
    {
        public Facility Facility { get; set; }
        public int SlotDurationMinutes { get; set; }
        public List<DailyAvailability> DaysAvailability { get; set; }
    }
}
