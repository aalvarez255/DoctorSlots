using DoctorSlots.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorSlots.Api.DTOs
{
    public class FacilitySlots
    {
        public FacilitySlots()
        {
            Slots = new List<Slot>();
        }

        public Facility Facility { get; set; }
        public List<Slot> Slots { get; set; }
    }
}
