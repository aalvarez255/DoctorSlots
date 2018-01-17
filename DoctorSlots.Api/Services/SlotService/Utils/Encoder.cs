using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorSlots.Api.Services.SlotService.Utils
{
    public class Encoder : IEncoder
    {
        public string EncodeBase64(string text)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }
    }
}
