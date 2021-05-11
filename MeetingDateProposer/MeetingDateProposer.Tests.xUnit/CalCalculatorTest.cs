using Xunit;
using MeetingDateProposer.Domain.Models;
using MeetingDateProposer.Domain.Utilities;
using MeetingDateProposer.BusinessLayer.Providers;
using System.Collections.Generic;
using System;

namespace MeetingDateProposer.Tests.xUnit
{
    public class CalCalculatorTest
    {
        [Fact]
        public void MeetingTestNestedEvents()
        {
            // Arrange
            Meeting meeting = new Meeting()
            {
                ConnectedUsers = new List<User>(),
                MeetingID = 1
            };
            var UserCal1 = new Calendar() { UserCalendar = new List<CalendarEvent>() };
            UserCal1.UserCalendar.Add(new CalendarEvent() { EventStart = new DateTime(2021, 5, 8, 12, 0, 0), EventEnd = new DateTime(2021, 5, 8, 14, 0, 0) });

            var UserCal2 = new Calendar() { UserCalendar = new List<CalendarEvent>() };
            UserCal2.UserCalendar.Add(new CalendarEvent() { EventStart = new DateTime(2021, 5, 8, 13, 0, 0), EventEnd = new DateTime(2021, 5, 8, 13, 30, 0) });

            meeting.ConnectedUsers.Add(new User() { Calendar = UserCal1, Credentials = null, UserId = 1 });
            meeting.ConnectedUsers.Add(new User() { Calendar = UserCal2, Credentials = null, UserId = 2 });

            var availableTime = new Calendar() { UserCalendar = new List<CalendarEvent>() };
            availableTime.UserCalendar.Add(new CalendarEvent() { EventStart = DateTime.MinValue, EventEnd = new DateTime(2021, 5, 8, 12, 0, 0) });
            availableTime.UserCalendar.Add(new CalendarEvent() { EventStart = new DateTime(2021, 5, 8, 14, 0, 0), EventEnd = DateTime.MaxValue });

            var calculatorUT = new CalendarCalculator();

            // Act
            var result = calculatorUT.CalculateAvailableMeetingTime(meeting);

            // Assert
            Assert.True(ObjectEquivalence.CalendarCheck(result, availableTime));
        }

        [Fact]
        public void MeetingTestIntersectedEvents()
        {
            // Arrange
            Meeting meeting = new Meeting()
            {
                ConnectedUsers = new List<User>(),
                MeetingID = 1
            };
            var UserCal1 = new Calendar() { UserCalendar = new List<CalendarEvent>() };
            UserCal1.UserCalendar.Add(new CalendarEvent() { EventStart = new DateTime(2021, 5, 8, 12, 0, 0), EventEnd = new DateTime(2021, 5, 8, 14, 0, 0) });

            var UserCal2 = new Calendar() { UserCalendar = new List<CalendarEvent>() };
            UserCal2.UserCalendar.Add(new CalendarEvent() { EventStart = new DateTime(2021, 5, 8, 13, 0, 0), EventEnd = new DateTime(2021, 5, 8, 15, 30, 0) });

            meeting.ConnectedUsers.Add(new User() { Calendar = UserCal1, Credentials = null, UserId = 1 });
            meeting.ConnectedUsers.Add(new User() { Calendar = UserCal2, Credentials = null, UserId = 2 });

            var availableTime = new Calendar() { UserCalendar = new List<CalendarEvent>() };
            availableTime.UserCalendar.Add(new CalendarEvent() { EventStart = DateTime.MinValue, EventEnd = new DateTime(2021, 5, 8, 12, 0, 0) });
            availableTime.UserCalendar.Add(new CalendarEvent() { EventStart = new DateTime(2021, 5, 8, 15, 30, 0), EventEnd = DateTime.MaxValue });

            var calculatorUT = new CalendarCalculator();

            // Act
            var result = calculatorUT.CalculateAvailableMeetingTime(meeting);

            // Assert
            Assert.True(ObjectEquivalence.CalendarCheck(result, availableTime));
        }

        [Fact]
        public void MeetingTestSpacedEvents()
        {
            // Arrange
            Meeting meeting = new Meeting()
            {
                ConnectedUsers = new List<User>(),
                MeetingID = 1
            };
            var UserCal1 = new Calendar() { UserCalendar = new List<CalendarEvent>() };
            UserCal1.UserCalendar.Add(new CalendarEvent() { EventStart = new DateTime(2021, 5, 8, 12, 0, 0), EventEnd = new DateTime(2021, 5, 8, 14, 0, 0) });

            var UserCal2 = new Calendar() { UserCalendar = new List<CalendarEvent>() };
            UserCal2.UserCalendar.Add(new CalendarEvent() { EventStart = new DateTime(2021, 5, 8, 15, 0, 0), EventEnd = new DateTime(2021, 5, 8, 16, 30, 0) });

            meeting.ConnectedUsers.Add(new User() { Calendar = UserCal1, Credentials = null, UserId = 1 });
            meeting.ConnectedUsers.Add(new User() { Calendar = UserCal2, Credentials = null, UserId = 2 });

            var availableTime = new Calendar() { UserCalendar = new List<CalendarEvent>() };
            availableTime.UserCalendar.Add(new CalendarEvent() { EventStart = DateTime.MinValue, EventEnd = new DateTime(2021, 5, 8, 12, 0, 0) });
            availableTime.UserCalendar.Add(new CalendarEvent() { EventStart = new DateTime(2021, 5, 8, 14, 0, 0), EventEnd = new DateTime(2021, 5, 8, 15, 0, 0) });
            availableTime.UserCalendar.Add(new CalendarEvent() { EventStart = new DateTime(2021, 5, 8, 16, 30, 0), EventEnd = DateTime.MaxValue });

            var calculatorUT = new CalendarCalculator();

            // Act
            var result = calculatorUT.CalculateAvailableMeetingTime(meeting);

            // Assert
            Assert.True(ObjectEquivalence.CalendarCheck(result, availableTime));
        }

        [Fact]
        public void MeetingTestSameDateEvents()
        {
            // Arrange
            Meeting meeting = new Meeting()
            {
                ConnectedUsers = new List<User>(),
                MeetingID = 1
            };
            var UserCal1 = new Calendar() { UserCalendar = new List<CalendarEvent>() };
            UserCal1.UserCalendar.Add(new CalendarEvent() { EventStart = new DateTime(2021, 5, 8, 12, 0, 0), EventEnd = new DateTime(2021, 5, 8, 13, 0, 0) });

            var UserCal2 = new Calendar() { UserCalendar = new List<CalendarEvent>() };
            UserCal2.UserCalendar.Add(new CalendarEvent() { EventStart = new DateTime(2021, 5, 8, 12, 0, 0), EventEnd = new DateTime(2021, 5, 8, 13, 00, 0) });

            meeting.ConnectedUsers.Add(new User() { Calendar = UserCal1, Credentials = null, UserId = 1 });
            meeting.ConnectedUsers.Add(new User() { Calendar = UserCal2, Credentials = null, UserId = 2 });

            var availableTime = new Calendar() { UserCalendar = new List<CalendarEvent>() };
            availableTime.UserCalendar.Add(new CalendarEvent() { EventStart = DateTime.MinValue, EventEnd = new DateTime(2021, 5, 8, 12, 0, 0) });
            availableTime.UserCalendar.Add(new CalendarEvent() { EventStart = new DateTime(2021, 5, 8, 13, 0, 0), EventEnd = DateTime.MaxValue });

            var calculatorUT = new CalendarCalculator();

            // Act
            var result = calculatorUT.CalculateAvailableMeetingTime(meeting);

            // Assert
            Assert.True(ObjectEquivalence.CalendarCheck(result, availableTime));
        }

        [Fact]
        public void MeetingTestNoEvents()
        {
            // Arrange
            Meeting meeting = new Meeting()
            {
                ConnectedUsers = new List<User>(),
                MeetingID = 1
            };
            var UserCal1 = new Calendar() { UserCalendar = new List<CalendarEvent>() };

            var UserCal2 = new Calendar() { UserCalendar = new List<CalendarEvent>() };

            meeting.ConnectedUsers.Add(new User() { Calendar = UserCal1, Credentials = null, UserId = 1 });
            meeting.ConnectedUsers.Add(new User() { Calendar = UserCal2, Credentials = null, UserId = 2 });

            var availableTime = new Calendar() { UserCalendar = new List<CalendarEvent>() };
            availableTime.UserCalendar.Add(new CalendarEvent() { EventStart = DateTime.MinValue, EventEnd = DateTime.MaxValue });

            var calculatorUT = new CalendarCalculator();

            // Act
            var result = calculatorUT.CalculateAvailableMeetingTime(meeting);

            // Assert
            Assert.True(ObjectEquivalence.CalendarCheck(result, availableTime));
        }

        [Fact]
        public void MeetingTestInfiniteEvents()
        {
            // Arrange
            Meeting meeting = new Meeting()
            {
                ConnectedUsers = new List<User>(),
                MeetingID = 1
            };
            var UserCal1 = new Calendar() { UserCalendar = new List<CalendarEvent>() };
            UserCal1.UserCalendar.Add(new CalendarEvent() { EventStart = DateTime.MinValue, EventEnd = new DateTime(2021, 5, 8, 12, 00, 0) });

            var UserCal2 = new Calendar() { UserCalendar = new List<CalendarEvent>() };
            UserCal2.UserCalendar.Add(new CalendarEvent() { EventStart = new DateTime(2021, 5, 8, 12, 0, 0), EventEnd = DateTime.MaxValue });

            meeting.ConnectedUsers.Add(new User() { Calendar = UserCal1, Credentials = null, UserId = 1 });
            meeting.ConnectedUsers.Add(new User() { Calendar = UserCal2, Credentials = null, UserId = 2 });

            var availableTime = new Calendar() { UserCalendar = new List<CalendarEvent>() };

            var calculatorUT = new CalendarCalculator();

            // Act
            var result = calculatorUT.CalculateAvailableMeetingTime(meeting);

            // Assert
            Assert.True(ObjectEquivalence.CalendarCheck(result, availableTime));
        }

        [Fact]
        public void MeetingTestOneUserOnly()
        {
            // Arrange
            Meeting meeting = new Meeting()
            {
                ConnectedUsers = new List<User>(),
                MeetingID = 1
            };
            var UserCal1 = new Calendar() { UserCalendar = new List<CalendarEvent>() };
            UserCal1.UserCalendar.Add(new CalendarEvent() { EventStart = new DateTime(2021, 5, 8, 12, 0, 0), EventEnd = new DateTime(2021, 5, 8, 14, 0, 0) });

            meeting.ConnectedUsers.Add(new User() { Calendar = UserCal1, Credentials = null, UserId = 1 });

            var availableTime = new Calendar() { UserCalendar = new List<CalendarEvent>() };
            availableTime.UserCalendar.Add(new CalendarEvent() { EventStart = DateTime.MinValue, EventEnd = new DateTime(2021, 5, 8, 12, 0, 0) });
            availableTime.UserCalendar.Add(new CalendarEvent() { EventStart = new DateTime(2021, 5, 8, 14, 0, 0), EventEnd = DateTime.MaxValue });

            var calculatorUT = new CalendarCalculator();

            // Act
            var result = calculatorUT.CalculateAvailableMeetingTime(meeting);

            // Assert
            Assert.True(ObjectEquivalence.CalendarCheck(result, availableTime));
        }

        [Fact]
        public void MeetingTestMultipleUsers()
        {
            // Arrange
            Meeting myMeeting = new Meeting();
            MeetingGenerator generator = new MeetingGenerator();
            CalendarCalculator calculator = new CalendarCalculator();

            // Act
            myMeeting = generator.GenerateMeeting(100, 100);
            var result = calculator.CalculateAvailableMeetingTime(myMeeting);

            // Assert
            Assert.NotEmpty(result.UserCalendar);
            foreach(var user in myMeeting.ConnectedUsers)
            {
                foreach (var cal in user.Calendar.UserCalendar)
                {
                    Assert.DoesNotContain(cal, result.UserCalendar);
                }
            }
        }

        [Fact]
        public void MeetingTestReversedEventsException()
        {
            // Arrange
            Meeting meeting = new Meeting()
            {
                ConnectedUsers = new List<User>(),
                MeetingID = 1
            };
            var UserCal1 = new Calendar() { UserCalendar = new List<CalendarEvent>() };
            UserCal1.UserCalendar.Add(new CalendarEvent() { EventStart = new DateTime(2021, 5, 8, 14, 0, 0), EventEnd = new DateTime(2021, 5, 8, 12, 0, 0) });

            var UserCal2 = new Calendar() { UserCalendar = new List<CalendarEvent>() };
            UserCal2.UserCalendar.Add(new CalendarEvent() { EventStart = new DateTime(2021, 5, 8, 13, 0, 0), EventEnd = new DateTime(2021, 5, 8, 13, 30, 0) });

            meeting.ConnectedUsers.Add(new User() { Calendar = UserCal1, Credentials = null, UserId = 1 });
            meeting.ConnectedUsers.Add(new User() { Calendar = UserCal2, Credentials = null, UserId = 2 });

            var calculatorUT = new CalendarCalculator();

            // Act
            Action test = () => calculatorUT.CalculateAvailableMeetingTime(meeting);

            // Assert
            Assert.ThrowsAny<Exception>(test);
        }

        
    }
}