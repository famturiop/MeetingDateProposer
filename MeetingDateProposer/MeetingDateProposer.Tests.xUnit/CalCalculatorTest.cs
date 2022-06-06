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
        private CalendarEvent CreateCalendarEvent(DateTime start, DateTime end)
        {
            return new CalendarEvent()
            {
                EventStart = start,
                EventEnd = end
            };
        }

        private Calendar CreateCalendar(params CalendarEvent[] calendarEvents)
        {
            var calendar = new Calendar
            {
                UserCalendar = new List<CalendarEvent>()
            };

            foreach (var calEvent in calendarEvents)
            {
                calendar.UserCalendar.Add(calEvent);
            }

            return calendar;
        }

        private ApplicationUser CreateAppUser(params Calendar[] calendars)
        {
            var user = new ApplicationUser
            {
                Calendars = new List<Calendar>(),
                Credentials = null,
                Id = Guid.NewGuid()
            };

            foreach (var calendar in calendars)
            {
                user.Calendars.Add(calendar);
            }

            return user;
        }

        private Meeting CreateMeeting(params ApplicationUser[] applicationUsers)
        {
            var meeting = new Meeting
            {
                ConnectedUsers = new List<ApplicationUser>(),
                Id = Guid.NewGuid()
            };

            foreach (var user in applicationUsers)
            {
                meeting.ConnectedUsers.Add(user);
            }

            return meeting;
        }

        private void CompareMultipleCalendars(Calendar availableTime, params Calendar[] userCalendars)
        {
            var users = new List<ApplicationUser>();

            foreach (var calendar in userCalendars)
            {
                users.Add(CreateAppUser(calendar));
            }

            var meeting = CreateMeeting(users.ToArray());

            var calculatorUt = new CalendarCalculator();

            var result = calculatorUt.CalculateAvailableMeetingTime(meeting);

            Assert.True(ObjectEquivalence.CalendarCheck(result, availableTime));
        }

        [Fact]
        public void MeetingTestNestedEvents()
        {
            var calendar1 = CreateCalendar(
                CreateCalendarEvent(new DateTime(2021, 5, 8, 12, 0, 0), new DateTime(2021, 5, 8, 14, 0, 0)));

            var calendar2 = CreateCalendar(
                CreateCalendarEvent(new DateTime(2021, 5, 8, 13, 0, 0), new DateTime(2021, 5, 8, 13, 30, 0)));

            var resultCalendar = CreateCalendar(CreateCalendarEvent(DateTime.MinValue, new DateTime(2021, 5, 8, 12, 0, 0)),
                CreateCalendarEvent(new DateTime(2021, 5, 8, 14, 0, 0), DateTime.MaxValue));

            CompareMultipleCalendars(resultCalendar, calendar1, calendar2);
        }

        [Fact]
        public void MeetingTestIntersectedEvents()
        {
            var calendar1 = CreateCalendar(
                CreateCalendarEvent(new DateTime(2021, 5, 8, 12, 0, 0), new DateTime(2021, 5, 8, 14, 0, 0)));

            var calendar2 = CreateCalendar(
                CreateCalendarEvent(new DateTime(2021, 5, 8, 13, 0, 0), new DateTime(2021, 5, 8, 15, 30, 0)));

            var resultCalendar = CreateCalendar(CreateCalendarEvent(DateTime.MinValue, new DateTime(2021, 5, 8, 12, 0, 0)),
                CreateCalendarEvent(new DateTime(2021, 5, 8, 15, 30, 0), DateTime.MaxValue));

            CompareMultipleCalendars(resultCalendar, calendar1, calendar2);
        }

        [Fact]
        public void MeetingTestSpacedEvents()
        {
            var calendar1 = CreateCalendar(
                CreateCalendarEvent(new DateTime(2021, 5, 8, 12, 0, 0), new DateTime(2021, 5, 8, 14, 0, 0)));

            var calendar2 = CreateCalendar(
                CreateCalendarEvent(new DateTime(2021, 5, 8, 15, 0, 0), new DateTime(2021, 5, 8, 16, 30, 0)));

            var resultCalendar = CreateCalendar(CreateCalendarEvent(DateTime.MinValue, new DateTime(2021, 5, 8, 12, 0, 0)),
                CreateCalendarEvent(new DateTime(2021, 5, 8, 14, 0, 0), new DateTime(2021, 5, 8, 15, 0, 0)),
                CreateCalendarEvent(new DateTime(2021, 5, 8, 16, 30, 0), DateTime.MaxValue));

            CompareMultipleCalendars(resultCalendar, calendar1, calendar2);
        }

        [Fact]
        public void MeetingTestSameDateEvents()
        {
            var calendar1 = CreateCalendar(
                CreateCalendarEvent(new DateTime(2021, 5, 8, 12, 0, 0), new DateTime(2021, 5, 8, 13, 0, 0)));

            var calendar2 = CreateCalendar(
                CreateCalendarEvent(new DateTime(2021, 5, 8, 12, 0, 0), new DateTime(2021, 5, 8, 13, 0, 0)));

            var resultCalendar = CreateCalendar(CreateCalendarEvent(DateTime.MinValue, new DateTime(2021, 5, 8, 12, 0, 0)),
                CreateCalendarEvent(new DateTime(2021, 5, 8, 13, 0, 0), DateTime.MaxValue));

            CompareMultipleCalendars(resultCalendar, calendar1, calendar2);
        }

        [Fact]
        public void MeetingTestNoEvents()
        {
            var calendar1 = CreateCalendar();

            var calendar2 = CreateCalendar();

            var resultCalendar = CreateCalendar(CreateCalendarEvent(DateTime.MinValue, DateTime.MaxValue));

            CompareMultipleCalendars(resultCalendar, calendar1, calendar2);
        }

        [Fact]
        public void MeetingTestInfiniteEvents()
        {
            var calendar1 = CreateCalendar(
                CreateCalendarEvent(DateTime.MinValue, new DateTime(2021, 5, 8, 12, 0, 0)));

            var calendar2 = CreateCalendar(
                CreateCalendarEvent(new DateTime(2021, 5, 8, 12, 0, 0), DateTime.MaxValue));

            var resultCalendar = CreateCalendar();

            CompareMultipleCalendars(resultCalendar, calendar1, calendar2);
        }

        [Fact]
        public void MeetingTestOneUserOnly()
        {
            var calendar1 = CreateCalendar(
                CreateCalendarEvent(new DateTime(2021, 5, 8, 12, 0, 0), new DateTime(2021, 5, 8, 14, 0, 0)));

            var resultCalendar = CreateCalendar(CreateCalendarEvent(DateTime.MinValue, new DateTime(2021, 5, 8, 12, 0, 0)),
                CreateCalendarEvent(new DateTime(2021, 5, 8, 14, 0, 0), DateTime.MaxValue));

            CompareMultipleCalendars(resultCalendar, calendar1);
        }

        [Fact]
        public void MeetingTestMultipleUsers()
        {
            var generator = new MeetingGenerator();
            var calculator = new CalendarCalculator();

            var myMeeting = MeetingGenerator.GenerateMeeting(100, 100);
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
            var user1 = CreateAppUser(CreateCalendar(
                CreateCalendarEvent(new DateTime(2021, 5, 8, 14, 0, 0), new DateTime(2021, 5, 8, 12, 0, 0))));

            var user2 = CreateAppUser(CreateCalendar(
                CreateCalendarEvent(new DateTime(2021, 5, 8, 13, 0, 0), new DateTime(2021, 5, 8, 13, 30, 0))));

            var meeting = CreateMeeting(user1, user2);

            var calculatorUt = new CalendarCalculator();

            Assert.ThrowsAny<ArgumentException>(() =>
                calculatorUt.CalculateAvailableMeetingTime(meeting));
        }
    }
}