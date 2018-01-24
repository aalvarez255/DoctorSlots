using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DoctorSlots.Api.Tests
{
    public static class BaseTests
    {
        public static string GetTestJsonString()
        {
            string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "test.json");
            return File.ReadAllText(jsonPath);
        }
    }
}
