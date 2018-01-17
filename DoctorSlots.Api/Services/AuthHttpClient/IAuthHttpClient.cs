using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorSlots.Api.Services
{
    public interface IAuthHttpClient
    {
        /* Performs a GET request to the provided URL using Authorization header */
        Task<T> GetAsync<T>(string url); 
    }
}
