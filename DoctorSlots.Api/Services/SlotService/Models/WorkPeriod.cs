using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorSlots.Api.SlotService.Models
{
    public class WorkPeriod
    {
        public int StartHour { get; set; }      //Morning opening hour(from 0 to 23)   
        public int LunchStartHour { get; set; } //Morning closing hour(from 0 to 23)
        public int LunchEndHour { get; set; }   //Afternoon opening hour(from 0 to 23)
        public int EndHour { get; set; }        //Afternoon closing hour(from 0 to 23)       
    }
}
