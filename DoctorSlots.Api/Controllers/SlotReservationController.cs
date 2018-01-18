using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DoctorSlots.Api.Controllers
{
    [Route("api/[controller]")]
    public class SlotReservationController : Controller
    {
        // GET api/values
        [HttpPost]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }        
    }
}
