using MeetingDateProposer.Domain.Models.ApplicationModels;
using System;
using System.Collections.Generic;

namespace MeetingDateProposer.Domain.Utilities
{
    public class MeetingGenerator
    {
        public static Meeting GenerateMeeting(int numberOfUsers, int numberOfEvents)
        {
            var testMeeting = new Meeting
            {
                ConnectedUsers = new List<ApplicationUser>()
            };

            for (int currentUser = 0; currentUser < numberOfUsers; currentUser++)
            {
                var testUser = new ApplicationUser()
                {
                    Id = Guid.NewGuid(),
                    Calendars = new List<Calendar> { GenerateCalendar(numberOfEvents) }
                };
                testMeeting.ConnectedUsers.Add(testUser);
            }

            return testMeeting;
        }

        public static Calendar GenerateCalendar(int numberOfEvents)
        {
            var testCalendar = new Calendar
            {
                UserCalendar = new List<CalendarEvent>()
            };

            var calendarEvent = new CalendarEvent
            {
                EventStart = DateTime.Now,
                EventEnd = DateTime.Now
            };

            for (int generateNextEvent = 0;
                generateNextEvent < numberOfEvents;
                generateNextEvent++)
            {
                calendarEvent = GenerateCalendarEvent(calendarEvent);
                testCalendar.UserCalendar.Add(calendarEvent);
            }

            return testCalendar;
        }

        private static CalendarEvent GenerateCalendarEvent(CalendarEvent prevEvent)
        {
            var prevEventTimeEnd = prevEvent.EventEnd;
            var time = (prevEventTimeEnd - DateTime.MinValue).TotalSeconds;
            var timePlusInterval = (prevEventTimeEnd - DateTime.MinValue + new TimeSpan(0, 2, 0, 0))
                .TotalSeconds;

            var rnd = new Random();
            var nextEventInsertedTimeSpan = TimeSpan.FromSeconds(
                time + (timePlusInterval - time) * rnd.NextDouble());
            var nextEventTimeStart = DateTime.MinValue.Add(nextEventInsertedTimeSpan);
            var nextEventTimeEnd = nextEventTimeStart.AddHours(
                rnd.Next(0, 3)).AddMinutes(rnd.Next(0, 59));

            return new CalendarEvent
            {
                EventStart = nextEventTimeStart,
                EventEnd = nextEventTimeEnd
            };

        }
    }
}