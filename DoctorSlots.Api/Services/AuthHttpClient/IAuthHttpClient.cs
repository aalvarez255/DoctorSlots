using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorSlots.Api.Services
{
    interface IAuthHttpClient
    {
        /* Performs a GET request to the provided URL using Basic Authorization header */
        Task<T> Get<T>(string url); 
    }
}
