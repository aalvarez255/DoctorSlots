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
            Func<HttpClient, Task<T>> getMethod = async (httpClient) => {
                T result = default(T);

                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseText = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<T>(responseText);
                }

                return result;
            };

            return await ApiRequest<T>(getMethod);
        }

        public async Task PostAsync<T>(string url, T data)
        {
            Func<HttpClient, Task<T>> postMethod = async (httpClient) => {
                var dataAsString = JsonConvert.SerializeObject(data);
                var content = new StringContent(dataAsString);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await httpClient.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                    throw new Exception(await response.Content.ReadAsStringAsync());

                return data;
            };

            await ApiRequest<T>(postMethod);
        }

        private async Task<T> ApiRequest<T>(Func<HttpClient, Task<T>> action)
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                // required to make request to a self-signed SSL certificate API
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                using (var client = new HttpClient(httpClientHandler))
                {
                    client.BaseAddress = new Uri(BaseAddress);
                    client.DefaultRequestHeaders.Authorization = GetAuthorizationHeader();

                    return await action(client);
                }
            }
        }

        protected abstract AuthenticationHeaderValue GetAuthorizationHeader();
    }
}
