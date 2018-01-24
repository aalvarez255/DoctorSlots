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
        public async Task<IActionResult> Post([FromBody]TakeSlot slotReservation)
        {
            try
            {
                if (slotReservation.Start.IsNullOrMin() ||
                    slotReservation.End.IsNullOrMin())
                    throw new Exception("Start or End date missing");

                if (slotReservation.Start >= slotReservation.End)
                    throw new Exception("Start date can't be greater than End date");

                await _slotService.PerformSlotReservation(slotReservation);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new ApiError(400, e.Message));
            }
        }
    }
}
