using DoctorSlots.Api.Services.SlotService.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorSlots.Api.Services.SlotService
{
    public class SlotHttpClient : AuthHttpClient
    {
        protected override T ParseResponse<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, new SlotJsonConverter());
        }
    }
}
