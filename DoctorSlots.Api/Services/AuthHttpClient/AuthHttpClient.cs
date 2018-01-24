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
        private readonly IHttpClientFactory _httpClientFactory;

        public abstract string BaseAddress { get; }

        public AuthHttpClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            T result = default(T);

            HttpResponseMessage response = await CreateHttpClient().GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responseText = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(responseText);
            }

            return result;
        }

        public async Task PostAsync(string url, object data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await CreateHttpClient().PostAsync(url, content);
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());
        }
        protected abstract AuthenticationHeaderValue GetAuthorizationHeader();

        private HttpClient CreateHttpClient()
        {
            return _httpClientFactory.Create(BaseAddress, GetAuthorizationHeader());
        }
    }
}
