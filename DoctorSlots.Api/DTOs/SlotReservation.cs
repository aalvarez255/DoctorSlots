using DoctorSlots.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorSlots.Api.DTOs
{
    public class SlotReservation
    {
        public string FacilityId { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public Patient Patient { get; set; }
        public string Comments { get; set; }
    }
}
