using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorSlots.Api.DTOs
{
    public class ApiError
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public ApiError(int code, string message)
        {
            this.Code = code;
            this.Message = message;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
