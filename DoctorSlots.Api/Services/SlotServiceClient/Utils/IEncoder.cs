using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorSlots.Api.Services.SlotServiceClient.Utils
{
    public interface IEncoder
    {
        string EncodeBase64(string text);
    }
}
