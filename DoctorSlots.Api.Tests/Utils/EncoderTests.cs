using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorSlots.Api.Tests.Utils
{
    [TestFixture]
    public class EncoderTests
    {
        private Api.Utils.Encoder _encoder;

        [SetUp]
        public void EncoderSetUp()
        {
            _encoder = new Api.Utils.Encoder();
        }

        [Test]
        public void EncodeBase64SuccessTest()
        {
            //Arrange
            string correctBase64 = "YmFzZTY0IGVuY29kZXIgd29ya3Mh";

            //Act
            string encodingResult = _encoder.EncodeBase64("base64 encoder works!");

            //Assert
            Assert.AreEqual(correctBase64, encodingResult);
        }

        [Test]
        public void EncodeBase64FailureTest()
        {
            //Arrange
            string correctBase64 = "YmFzZTY0IGVuY29kZXIgd29ya3Mh";

            //Act
            string encodingResult = _encoder.EncodeBase64("random text");

            //Assert
            Assert.AreNotEqual(correctBase64, encodingResult);
        }
    }
}
