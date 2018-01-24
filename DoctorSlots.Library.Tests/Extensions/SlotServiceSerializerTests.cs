using DoctorSlots.Api.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DoctorSlots.Api.Tests.Extensions
{
    [TestFixture]
    public class SlotServiceSerializerTests
    {
        [Test]
        public void ReadJsonReturnedTypeNotNullTest()
        {
            //Arrange
            var testJson = BaseTests.GetTestJsonString();

            //Act
            WeeklyAvailability result = JsonConvert.DeserializeObject<WeeklyAvailability>(testJson);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ReadJsonReturnedTypeCorrectFieldsTest()
        {
            //Arrange
            var testJson = BaseTests.GetTestJsonString();

            //Act
            WeeklyAvailability result = JsonConvert.DeserializeObject<WeeklyAvailability>(testJson);


            //Assert
            Assert.AreEqual(result.FacilityId, "e9f7bd81-965d-4464-b607-999112b56022");
            Assert.AreEqual(result.SlotDurationMinutes, 10);
            Assert.AreEqual(result.DaysAvailability.Count, 3);
        }
    }
}
