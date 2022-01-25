using MeetingDateProposer.BusinessLayer;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using MeetingDateProposer.Domain.Utilities;
using System;
using System.Collections.Generic;
using Xunit;

namespace MeetingDateProposer.Tests.xUnit
{
    public class CalCalculatorTest
    {
        [Fact]
        public void MeetingTestNestedEvents()
        {
            var userCalendar1 = new Calendar
            {
                UserCalendar = new List<CalendarEvent>
                {
                    new CalendarEvent
                    {
                        EventStart = new DateTime(2021, 5, 8, 12, 0, 0), 
                        EventEnd = new DateTime(2021, 5, 8, 14, 0, 0)
                    }
                }
            };
            
            var userCalendar2 = new Calendar
            {
                UserCalendar = new List<CalendarEvent>
                {
                    new CalendarEvent
                    {
                        EventStart = new DateTime(2021, 5, 8, 13, 0, 0), 
                        EventEnd = new DateTime(2021, 5, 8, 13, 30, 0) 
                    }
                }
            };

            var meeting = new Meeting
            {
                ConnectedUsers = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        Calendars = new List<Calendar>
                        {
                            userCalendar1
                        },
                        Credentials = null,
                        Id = Guid.NewGuid() 
                    },
                    new ApplicationUser
                    {
                        Calendars = new List<Calendar>
                        {
                            userCalendar2
                        }, 
                        Credentials = null, 
                        Id = Guid.NewGuid()
                    }
                },
                Id = Guid.NewGuid()
            };

            var availableTime = new Calendar
            {
                UserCalendar = new List<CalendarEvent>
                {
                    new CalendarEvent
                    {
                        EventStart = DateTime.MinValue, 
                        EventEnd = new DateTime(2021, 5, 8, 12, 0, 0)
                    },
                    new CalendarEvent
                    {
                        EventStart = new DateTime(2021, 5, 8, 14, 0, 0),
                        EventEnd = DateTime.MaxValue
                    }
                }
            };

            var calculatorUt = new CalendarCalculator();

            var result = calculatorUt.CalculateAvailableMeetingTime(meeting);

            Assert.True(ObjectEquivalence.CalendarCheck(result, availableTime));
        }

        [Fact]
        public void MeetingTestIntersectedEvents()
        {
            var userCalendar1 = new Calendar
            {
                UserCalendar = new List<CalendarEvent>
                {
                    new CalendarEvent
                    {
                        EventStart = new DateTime(2021, 5, 8, 12, 0, 0),
                        EventEnd = new DateTime(2021, 5, 8, 14, 0, 0)
                    }
                }
            };

            var userCalendar2 = new Calendar
            {
                UserCalendar = new List<CalendarEvent>
                {
                    new CalendarEvent
                    {
                        EventStart = new DateTime(2021, 5, 8, 13, 0, 0),
                        EventEnd = new DateTime(2021, 5, 8, 15, 30, 0)
                    }
                }
            };

            var meeting = new Meeting
            {
                ConnectedUsers = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        Calendars = new List<Calendar>
                        {
                            userCalendar1
                        },
                        Credentials = null,
                        Id = Guid.NewGuid()
                    },
                    new ApplicationUser
                    {
                        Calendars = new List<Calendar>
                        {
                            userCalendar2
                        },
                        Credentials = null,
                        Id = Guid.NewGuid()
                    }
                },
                Id = Guid.NewGuid()
            };

            var availableTime = new Calendar
            {
                UserCalendar = new List<CalendarEvent>
                {
                    new CalendarEvent
                    {
                        EventStart = DateTime.MinValue, 
                        EventEnd = new DateTime(2021, 5, 8, 12, 0, 0)
                    },
                    new CalendarEvent
                    {
                        EventStart = new DateTime(2021, 5, 8, 15, 30, 0),
                        EventEnd = DateTime.MaxValue
                    }
                }
            };

            var calculatorUt = new CalendarCalculator();

            var result = calculatorUt.CalculateAvailableMeetingTime(meeting);

            Assert.True(ObjectEquivalence.CalendarCheck(result, availableTime));
        }

        [Fact]
        public void MeetingTestSpacedEvents()
        {
            var userCalendar1 = new Calendar
            {
                UserCalendar = new List<CalendarEvent>
                {
                    new CalendarEvent
                    {
                        EventStart = new DateTime(2021, 5, 8, 12, 0, 0),
                        EventEnd = new DateTime(2021, 5, 8, 14, 0, 0)
                    }
                }
            };

            var userCalendar2 = new Calendar
            {
                UserCalendar = new List<CalendarEvent>
                {
                    new CalendarEvent
                    {
                        EventStart = new DateTime(2021, 5, 8, 15, 0, 0),
                        EventEnd = new DateTime(2021, 5, 8, 16, 30, 0)
                    }
                }
            };

            var meeting = new Meeting
            {
                ConnectedUsers = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        Calendars = new List<Calendar>
                        {
                            userCalendar1
                        },
                        Credentials = null,
                        Id = Guid.NewGuid()
                    },
                    new ApplicationUser
                    {
                        Calendars = new List<Calendar>
                        {
                            userCalendar2
                        },
                        Credentials = null,
                        Id = Guid.NewGuid()
                    }
                },
                Id = Guid.NewGuid()
            };

            var availableTime = new Calendar
            {
                UserCalendar = new List<CalendarEvent>
                {
                    new CalendarEvent
                    {
                        EventStart = DateTime.MinValue,
                        EventEnd = new DateTime(2021, 5, 8, 12, 0, 0)
                    },
                    new CalendarEvent
                    {
                        EventStart = new DateTime(2021, 5, 8, 14, 0, 0), 
                        EventEnd = new DateTime(2021, 5, 8, 15, 0, 0)
                    },
                    new CalendarEvent
                    {
                        EventStart = new DateTime(2021, 5, 8, 16, 30, 0),
                        EventEnd = DateTime.MaxValue
                    }
                }
            };
            
            var calculatorUt = new CalendarCalculator();

            var result = calculatorUt.CalculateAvailableMeetingTime(meeting);

            Assert.True(ObjectEquivalence.CalendarCheck(result, availableTime));
        }

        [Fact]
        public void MeetingTestSameDateEvents()
        {
            var userCalendar1 = new Calendar
            {
                UserCalendar = new List<CalendarEvent>
                {
                    new CalendarEvent
                    {
                        EventStart = new DateTime(2021, 5, 8, 12, 0, 0), 
                        EventEnd = new DateTime(2021, 5, 8, 13, 0, 0)
                    }
                }
            };

            var userCalendar2 = new Calendar
            {
                UserCalendar = new List<CalendarEvent>
                {
                    new CalendarEvent
                    {
                        EventStart = new DateTime(2021, 5, 8, 12, 0, 0),
                        EventEnd = new DateTime(2021, 5, 8, 13, 00, 0)
                    }
                }
            };

            var meeting = new Meeting
            {
                ConnectedUsers = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        Calendars = new List<Calendar>
                        {
                            userCalendar1
                        },
                        Credentials = null,
                        Id = Guid.NewGuid()
                    },
                    new ApplicationUser
                    {
                        Calendars = new List<Calendar>
                        {
                            userCalendar2
                        },
                        Credentials = null,
                        Id = Guid.NewGuid()
                    }
                },
                Id = Guid.NewGuid()
            };

            var availableTime = new Calendar
            {
                UserCalendar = new List<CalendarEvent>
                {
                    new CalendarEvent
                    {
                        EventStart = DateTime.MinValue, 
                        EventEnd = new DateTime(2021, 5, 8, 12, 0, 0)
                    },
                    new CalendarEvent
                    {
                        EventStart = new DateTime(2021, 5, 8, 13, 0, 0),
                        EventEnd = DateTime.MaxValue
                    }
                }
            };

            var calculatorUt = new CalendarCalculator();

            var result = calculatorUt.CalculateAvailableMeetingTime(meeting);

            Assert.True(ObjectEquivalence.CalendarCheck(result, availableTime));
        }

        [Fact]
        public void MeetingTestNoEvents()
        {
            var userCalendar1 = new Calendar
            {
                UserCalendar = new List<CalendarEvent>()
            };

            var userCalendar2 = new Calendar
            {
                UserCalendar = new List<CalendarEvent>()
            };

            var meeting = new Meeting
            {
                ConnectedUsers = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        Calendars = new List<Calendar>
                        {
                            userCalendar1
                        },
                        Credentials = null,
                        Id = Guid.NewGuid()
                    },
                    new ApplicationUser
                    {
                        Calendars = new List<Calendar>
                        {
                            userCalendar2
                        },
                        Credentials = null,
                        Id = Guid.NewGuid()
                    }
                },
                Id = Guid.NewGuid()
            };

            var availableTime = new Calendar
            {
                UserCalendar = new List<CalendarEvent>
                {
                    new CalendarEvent
                    {
                        EventStart = DateTime.MinValue,
                        EventEnd = DateTime.MaxValue
                    }
                }
            };

            var calculatorUt = new CalendarCalculator();

            var result = calculatorUt.CalculateAvailableMeetingTime(meeting);

            Assert.True(ObjectEquivalence.CalendarCheck(result, availableTime));
        }

        [Fact]
        public void MeetingTestInfiniteEvents()
        {
            var userCalendar1 = new Calendar
            {
                UserCalendar = new List<CalendarEvent>
                {
                    new CalendarEvent
                    {
                        EventStart = DateTime.MinValue, 
                        EventEnd = new DateTime(2021, 5, 8, 12, 00, 0)
                    }
                }
            };

            var userCalendar2 = new Calendar
            {
                UserCalendar = new List<CalendarEvent>
                {
                    new CalendarEvent
                    {
                        EventStart = new DateTime(2021, 5, 8, 12, 0, 0),
                        EventEnd = DateTime.MaxValue
                    }
                }
            };

            var meeting = new Meeting
            {
                ConnectedUsers = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        Calendars = new List<Calendar>
                        {
                            userCalendar1
                        },
                        Credentials = null,
                        Id = Guid.NewGuid()
                    },
                    new ApplicationUser
                    {
                        Calendars = new List<Calendar>
                        {
                            userCalendar2
                        },
                        Credentials = null,
                        Id = Guid.NewGuid()
                    }
                },
                Id = Guid.NewGuid()
            };

            var availableTime = new Calendar
            {
                UserCalendar = new List<CalendarEvent>()
            };

            var calculatorUt = new CalendarCalculator();

            var result = calculatorUt.CalculateAvailableMeetingTime(meeting);

            Assert.True(ObjectEquivalence.CalendarCheck(result, availableTime));
        }

        [Fact]
        public void MeetingTestOneUserOnly()
        {
            var userCalendar1 = new Calendar
            {
                UserCalendar = new List<CalendarEvent>
                {
                    new CalendarEvent
                    {
                        EventStart = new DateTime(2021, 5, 8, 12, 0, 0),
                        EventEnd = new DateTime(2021, 5, 8, 14, 0, 0)
                    }
                }
            };

            var meeting = new Meeting
            {
                ConnectedUsers = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        Calendars = new List<Calendar>
                        {
                            userCalendar1
                        },
                        Credentials = null,
                        Id = Guid.NewGuid()
                    }
                },
                Id = Guid.NewGuid()
            };

            var availableTime = new Calendar
            {
                UserCalendar = new List<CalendarEvent>
                {
                    new CalendarEvent
                    {
                        EventStart = DateTime.MinValue, 
                        EventEnd = new DateTime(2021, 5, 8, 12, 0, 0)
                    },
                    new CalendarEvent
                    {
                        EventStart = new DateTime(2021, 5, 8, 14, 0, 0),
                        EventEnd = DateTime.MaxValue
                    }
                }
            };
            
            var calculatorUt = new CalendarCalculator();

            var result = calculatorUt.CalculateAvailableMeetingTime(meeting);

            Assert.True(ObjectEquivalence.CalendarCheck(result, availableTime));
        }

        [Fact]
        public void MeetingTestMultipleUsers()
        {
            var generator = new MeetingGenerator();
            var calculator = new CalendarCalculator();

            var myMeeting = generator.GenerateMeeting(100, 100);
            var result = calculator.CalculateAvailableMeetingTime(myMeeting);

            Assert.NotEmpty(result.UserCalendar);
            foreach (var user in myMeeting.ConnectedUsers)
            {
                foreach (var cal in user.Calendars[0].UserCalendar)
                {
                    Assert.DoesNotContain(cal, result.UserCalendar);
                }
            }
        }

        [Fact]
        public void MeetingTestReversedEventsException()
        {
            var userCalendar1 = new Calendar
            {
                UserCalendar = new List<CalendarEvent>
                {
                    new CalendarEvent
                    {
                        EventStart = new DateTime(2021, 5, 8, 14, 0, 0), 
                        EventEnd = new DateTime(2021, 5, 8, 12, 0, 0)
                    }
                }
            };

            var userCalendar2 = new Calendar
            {
                UserCalendar = new List<CalendarEvent>
                {
                    new CalendarEvent
                    {
                        EventStart = new DateTime(2021, 5, 8, 13, 0, 0), 
                        EventEnd = new DateTime(2021, 5, 8, 13, 30, 0)
                    }
                }
            };

            var meeting = new Meeting
            {
                ConnectedUsers = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        Calendars = new List<Calendar>
                        {
                            userCalendar1
                        },
                        Credentials = null,
                        Id = Guid.NewGuid()
                    },
                    new ApplicationUser
                    {
                        Calendars = new List<Calendar>
                        {
                            userCalendar2
                        },
                        Credentials = null,
                        Id = Guid.NewGuid()
                    }
                },
                Id = Guid.NewGuid()
            };

            var calculatorUt = new CalendarCalculator();

            Assert.ThrowsAny<Exception>(() =>
                calculatorUt.CalculateAvailableMeetingTime(meeting));
        }
    }
}