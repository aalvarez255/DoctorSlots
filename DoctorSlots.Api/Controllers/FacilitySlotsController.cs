using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorSlots.Api.DTOs;
using DoctorSlots.Api.Services;
using DoctorSlots.Api.Services.SlotParser;
using DoctorSlots.Api.SlotServiceClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoctorSlots.Api.Controllers
{
    [Route("api/[controller]")]
    public class FacilitySlotsController : Controller
    {
        private readonly IAuthHttpClient _httpClient;
        private readonly ISlotConverter _slotConverter;

        public FacilitySlotsController(
            IAuthHttpClient httpClient, 
            ISlotConverter slotConverter)
        {
            _httpClient = httpClient;
            _slotConverter = slotConverter;
        }
        
        [HttpGet]
        public async Task<FacilitySlots> Get()
        {
            var availability = await _httpClient.GetAsync<WeeklyAvailability>(
                            "availability/GetWeeklyAvailability/20180115");

            var slots = _slotConverter.ParseWorkPeriods(
                            availability, 
                            DateTime.ParseExact("20180115", "yyyyMMdd", null));

            return new FacilitySlots() {
                Facility = availability.Facility,
                Slots = slots
            };
        }        
    }
}
