using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DoctorSlots.Api.Services
{
    public interface IHttpClientFactory
    {
        HttpClient Create(string baseAddress, AuthenticationHeaderValue authHeader);
    }
}
