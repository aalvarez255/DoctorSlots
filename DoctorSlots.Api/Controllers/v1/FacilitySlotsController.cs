﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorSlots.Api.Services;
using DoctorSlots.Api.SlotService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoctorSlots.Api.Controllers
{
    [Route("api/[controller]")]
    public class FacilitySlotsController : Controller
    {
        private readonly IAuthHttpClient _httpClient;

        public FacilitySlotsController(IAuthHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var result = await _httpClient.GetAsync<WeeklyAvailability>("/availability/GetWeeklyAvailability/20180115");
            return new string[] { "value1", "value2" };
        }        
    }
}
