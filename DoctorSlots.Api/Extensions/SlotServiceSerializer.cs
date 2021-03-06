﻿using DoctorSlots.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DoctorSlots.Api.Services.Extensions
{
    public class SlotServiceSerializer : JsonConverter
    {
        private readonly string[] _days = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(WeeklyAvailability);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);

            WeeklyAvailability availability = new WeeklyAvailability
            {
                SlotDurationMinutes = jsonObject["SlotDurationMinutes"].ToObject<int>(),
                FacilityId = ((JObject)jsonObject["Facility"])["FacilityId"].ToObject<string>()
            };

            // Deserialize days availability
            for (int i = 0; i < _days.Length; ++i) 
            {
                JObject jsonDay = (JObject)jsonObject[_days[i]];
                if (jsonDay == null) continue;

                availability.DaysAvailability.Add(new DailyAvailability()
                {
                    DayOfWeek = i,
                    WorkPeriod = jsonDay["WorkPeriod"]?.ToObject<WorkPeriod>(),
                    BusySlots = jsonDay["BusySlots"]?.ToObject<List<Slot>>()
                });
            }

            return availability;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
