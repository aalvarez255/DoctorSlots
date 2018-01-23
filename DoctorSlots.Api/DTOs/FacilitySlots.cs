using DoctorSlots.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorSlots.Api.DTOs
{
    public class FacilitySlots
    {
        public string FacilityId { get; set; }
        public List<Slot> Slots { get; set; }
    }
}
