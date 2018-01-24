using NUnit.Framework;
using System;
using DoctorSlots.Api.Extensions;

namespace DoctorSlots.Api.Tests.Extensions
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [Test]
        public void IsNullOrMinSuccessTest()
        {
            //Arrange
            DateTime dateTime = DateTime.MinValue;

            //Act
            var result = DateTimeExtensions.IsNullOrMin(dateTime);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsNullOrMinFailureTest()
        {
            //Arrange
            DateTime dateTime = DateTime.Now;

            //Act
            var result = DateTimeExtensions.IsNullOrMin(dateTime);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GetMondayOfWeekSuccessTest()
        {
            //Arrange
            DateTime testDate = DateTime.ParseExact("28/01/2018", "dd/MM/yyyy", null);

            //Act
            var result = DateTimeExtensions.GetMondayOfWeek(testDate);

            //Assert
            string mondayOfWeek = "22/01/2018";
            Assert.AreEqual(result.ToString("dd/MM/yyyy"), mondayOfWeek);
        }
    }
}
