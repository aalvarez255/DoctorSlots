using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DoctorSlots.Api.Services
{
    public class AuthHttpClient : IAuthHttpClient
    {
        public async Task<T> Get<T>(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                T result = default(T);
                
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseText = await response.Content.ReadAsStringAsync();
                    result = ParseResponse<T>(responseText);
                }

                return result;
            }
        }

        protected virtual T ParseResponse<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
