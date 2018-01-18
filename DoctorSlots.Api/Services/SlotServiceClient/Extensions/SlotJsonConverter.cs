using DoctorSlots.Api.SlotServiceClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DoctorSlots.Api.Services.SlotServiceClient.Extensions
{
    public class SlotJsonConverter : JsonConverter
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
                // Deserialize fields with same name
                Facility = jsonObject["Facility"].ToObject<Facility>(),
                SlotDurationMinutes = jsonObject["SlotDurationMinutes"].ToObject<int>()
            };

            // Deserialize days availability
            for (int i = 0; i < _days.Length; ++i) 
            {
                JObject jsonDay = (JObject)jsonObject[_days[i]];
                if (jsonDay == null) continue;

                availability.DaysAvailability.Add(new DailyAvailability()
                {
                    DayOfWeek = i % 6,
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
