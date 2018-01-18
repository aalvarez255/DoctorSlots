using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DoctorSlots.Api.Services
{
    public abstract class AuthHttpClient : IAuthHttpClient
    {
        public abstract string BaseAddress { get; }

        public async Task<T> GetAsync<T>(string url)
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                // required to make request to a self-signed SSL certificate API
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>  true;
                using (var client = new HttpClient(httpClientHandler))
                {
                    client.BaseAddress = new Uri(BaseAddress);
                    client.DefaultRequestHeaders.Authorization = GetAuthorizationHeader();
                    T result = default(T);

                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<T>(responseText);
                    }

                    return result;
                }
            }
        }

        protected abstract AuthenticationHeaderValue GetAuthorizationHeader();
    }
}
