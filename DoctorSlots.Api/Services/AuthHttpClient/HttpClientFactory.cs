using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DoctorSlots.Api.Services
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public HttpClient Create(string baseAddress, AuthenticationHeaderValue authHeader)
        {
            var httpClientHandler = new HttpClientHandler
            {
                // required to make request to a self-signed SSL certificate API
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            var client = new HttpClient(httpClientHandler)
            {
                BaseAddress = new Uri(baseAddress),
            };

            client.DefaultRequestHeaders.Authorization = authHeader;

            return client;
        }
    }
}
