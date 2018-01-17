using DoctorSlots.Api.SlotService.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DoctorSlots.Api.Services.SlotService.Extensions
{
    public class SlotJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(WeeklyAvailability).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                JObject item = JObject.Load(reader);

                //if (item["users"] != null)
                //{
                //    var users = item["users"].ToObject<IList<User>>(serializer);

                //    int length = item["length"].Value<int>();
                //    int limit = item["limit"].Value<int>();
                //    int start = item["start"].Value<int>();
                //    int total = item["total"].Value<int>();

                //    return new PagedList<User>(users, new PagingInformation(start, limit, length, total));
                //}
            }
            else
            {
                JArray array = JArray.Load(reader);

                //var users = array.ToObject<IList<User>>();

                //return new PagedList<User>(users);
            }

            // This should not happen. Perhaps better to throw exception at this point?
            return null;
        }
    }
}
