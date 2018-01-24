using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorSlots.Api.DTOs;
using DoctorSlots.Api.Extensions;
using DoctorSlots.Api.Models;
using DoctorSlots.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoctorSlots.Api.Controllers
{
    [Route("api/[controller]")]
    public class SlotReservationController : Controller
    {
        private readonly ISlotService _slotService;

        public SlotReservationController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SlotReservation slotReservation)
        {
            try
            {
                if (slotReservation == null)
                    throw new Exception("Invalid parameters");

                //mapping
                TakeSlot takeSlot = new TakeSlot()
                {
                    Comments = slotReservation.Comments,
                    Patient = slotReservation.Patient,
                    FacilityId = slotReservation.FacilityId,
                    Start = DateTime.ParseExact(slotReservation.Start, "dd/MM/yyyy HH:mm:ss", null),
                    End = DateTime.ParseExact(slotReservation.End, "dd/MM/yyyy HH:mm:ss", null)
                };

                await _slotService.PerformSlotReservation(takeSlot);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new ApiError(400, e.Message));
            }
        }
    }
}
