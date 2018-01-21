using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorSlots.Api.DTOs;
using DoctorSlots.Api.Services;
using DoctorSlots.Api.Services.SlotParser;
using DoctorSlots.Api.Models;
using Microsoft.AspNetCore.Mvc;
using DoctorSlots.Api.Extensions;

namespace DoctorSlots.Api.Controllers
{
    [Route("api/[controller]")]
    public class FacilitySlotsController : Controller
    {
        private readonly ISlotService _slotService;

        public FacilitySlotsController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        [HttpGet("{date}")]
        public async Task<IActionResult> Get(DateTime date)
        {
            if (date == null || date == DateTime.MinValue)
                return BadRequest(new ApiError(400, "Incorrect or missing parameter 'date'"));

            var availability = await _slotService.GetWeeklyAvailability(date);
            var slots = _slotService.ParseWorkPeriods(availability, date);

            return new OkObjectResult(new FacilitySlots() {
                Facility = availability.Facility,
                Slots = slots
            });
        }        
    }
}
