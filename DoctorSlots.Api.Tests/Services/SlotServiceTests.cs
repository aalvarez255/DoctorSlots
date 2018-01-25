using DoctorSlots.Api.Models;
using DoctorSlots.Api.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorSlots.Api.Tests.Services
{
    [TestFixture]
    public class SlotServiceTest
    {
        private Mock<IAuthHttpClient> _authHttpClientMock;

        private SlotService _slotService;

        [SetUp]
        public void SlotServiceSetup()
        {
            _authHttpClientMock = new Mock<IAuthHttpClient>();

            _slotService = new SlotService(_authHttpClientMock.Object);
        }

        [Test]
        public void GetWeeklyAvailabilitySuccessTest()
        {
            //Arrange
            _authHttpClientMock.Setup(x => x.GetAsync<WeeklyAvailability>(It.IsAny<string>()))
                .ReturnsAsync(() => new WeeklyAvailability());

            //Act
            var result = _slotService.GetWeeklyAvailability(DateTime.Now);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ParseWorkPeriodsResultValidSlotsCountTest()
        {
            //Arrange
            WeeklyAvailability testAvailability = new WeeklyAvailability()
            {
                SlotDurationMinutes = 30,
                DaysAvailability = new List<DailyAvailability>() {
                    new DailyAvailability() {
                        DayOfWeek = 3,  //Thursday 25
                        WorkPeriod = new WorkPeriod() {
                            StartHour = 12,
                            LunchStartHour = 14,
                            LunchEndHour = 15,
                            EndHour = 16
                        }
                    }
                }
            };
            DateTime date = DateTime.ParseExact("23/01/2018", "dd/MM/yyyy", null); //Tuesday

            //Act
            var result = _slotService.ParseWorkPeriods(testAvailability, date);

            //Assert
            Assert.IsTrue(result.Count() == 6);
        }

        [Test]
        public void ParseWorkPeriodsResultValidDaysTest()
        {
            //Arrange
            WeeklyAvailability testAvailability = new WeeklyAvailability()
            {
                SlotDurationMinutes = 30,
                DaysAvailability = new List<DailyAvailability>() {
                    new DailyAvailability() {
                        DayOfWeek = 3,  //Thursday 25
                        WorkPeriod = new WorkPeriod() {
                            StartHour = 10,
                            LunchStartHour = 14,
                            LunchEndHour = 15,
                            EndHour = 18
                        }
                    },
                    new DailyAvailability() {
                        DayOfWeek = 4,  //Friday 26
                        WorkPeriod = new WorkPeriod() {
                            StartHour = 10,
                            LunchStartHour = 14,
                            LunchEndHour = 15,
                            EndHour = 18
                        }
                    },
                }
            };
            DateTime date = DateTime.ParseExact("23/01/2018", "dd/MM/yyyy", null); //Tuesday

            //Act
            var result = _slotService.ParseWorkPeriods(testAvailability, date);

            //Assert
            Assert.IsTrue(result.All(s => s.Start.Date.Day == 25 || s.Start.Date.Day == 26));
        }

        [Test]
        public void ParseWorkPeriodsFilterBusySlotsTest()
        {
            //Arrange
            WeeklyAvailability testAvailability = new WeeklyAvailability()
            {
                SlotDurationMinutes = 30,
                DaysAvailability = new List<DailyAvailability>() {
                    new DailyAvailability() {
                        DayOfWeek = 3,  //Thursday 25
                        WorkPeriod = new WorkPeriod() {
                            StartHour = 10,
                            LunchStartHour = 11,
                            LunchEndHour = 15,
                            EndHour = 16
                        },
                        BusySlots = new List<Slot>() {
                            new Slot()
                            {
                                Start = DateTime.ParseExact("25/01/2018 10:00:00", "dd/MM/yyyy HH:mm:ss", null),
                                End = DateTime.ParseExact("25/01/2018 10:30:00", "dd/MM/yyyy HH:mm:ss", null)
                            },
                            new Slot()
                            {
                                Start = DateTime.ParseExact("25/01/2018 15:30:00", "dd/MM/yyyy HH:mm:ss", null),
                                End = DateTime.ParseExact("25/01/2018 16:00:00", "dd/MM/yyyy HH:mm:ss", null)
                            }
                        }
                    }
                }
            };
            DateTime date = DateTime.ParseExact("23/01/2018", "dd/MM/yyyy", null); //Tuesday

            //Act
            var result = _slotService.ParseWorkPeriods(testAvailability, date);

            //Assert
            Assert.IsTrue(result.Count == 2);
            Assert.AreEqual(result.ElementAt(0).Start.ToString("dd/MM/yyyy HH:mm:ss"), "25/01/2018 10:30:00");
            Assert.AreEqual(result.ElementAt(1).Start.ToString("dd/MM/yyyy HH:mm:ss"), "25/01/2018 15:00:00");
        }


        [Test]
        public void ParseWorkPeriodsFilterPastDatesTest()
        {
            //Arrange
            WeeklyAvailability testAvailability = new WeeklyAvailability()
            {
                SlotDurationMinutes = 30,
                DaysAvailability = new List<DailyAvailability>() {
                 new DailyAvailability() {
                        DayOfWeek = 2,  //Wednesday 24
                        WorkPeriod = new WorkPeriod() {
                            StartHour = 8,
                            LunchStartHour = 11,
                            LunchEndHour = 15,
                            EndHour = 21
                        }
                    },
                    new DailyAvailability() {
                        DayOfWeek = 3,  //Thursday 25
                        WorkPeriod = new WorkPeriod() {
                            StartHour = 10,
                            LunchStartHour = 11,
                            LunchEndHour = 15,
                            EndHour = 16
                        }
                    }
                }
            };
            DateTime date = DateTime.ParseExact("25/01/2018 14:59:00", "dd/MM/yyyy HH:mm:ss", null); //Thursday

            //Act
            var result = _slotService.ParseWorkPeriods(testAvailability, date);

            //Assert
            Assert.IsTrue(result.Count == 2);
            Assert.AreEqual(result.ElementAt(0).Start.ToString("dd/MM/yyyy HH:mm:ss"), "25/01/2018 15:00:00");
            Assert.AreEqual(result.ElementAt(1).Start.ToString("dd/MM/yyyy HH:mm:ss"), "25/01/2018 15:30:00");
        }

        [Test]
        public async Task PerformSlotReservationInvalidDatesTest()
        {
            //Arrange
            _authHttpClientMock.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<TakeSlot>()))
                 .Verifiable();

            TakeSlot takeSlot = new TakeSlot()
            {
                Start = DateTime.ParseExact("25/01/2018 00:10:00", "dd/MM/yyyy HH:mm:ss", null),
                End = DateTime.ParseExact("25/01/2018 00:10:00", "dd/MM/yyyy HH:mm:ss", null)
            };

            try
            {
                //Act
                await _slotService.PerformSlotReservation(takeSlot);

                //Assert
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task PerformSlotReservationNullDatesTest()
        {
            //Arrange
            _authHttpClientMock.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<TakeSlot>()))
                 .Verifiable();

            TakeSlot takeSlot = new TakeSlot()
            {
                Start = DateTime.MinValue,
                End = DateTime.ParseExact("25/01/2018 00:10:00", "dd/MM/yyyy HH:mm:ss", null)
            };

            try
            {
                //Act
                await _slotService.PerformSlotReservation(takeSlot);

                //Assert
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.Pass();
            }
        }
    }
}
