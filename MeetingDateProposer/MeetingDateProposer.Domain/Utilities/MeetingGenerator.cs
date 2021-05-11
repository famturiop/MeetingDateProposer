using System;
using System.Collections.Generic;
using MeetingDateProposer.Domain.Models;

namespace MeetingDateProposer.Domain.Utilities
{
    public class MeetingGenerator
    {

        public Meeting GenerateMeeting(int numberofUsers, int numberofEvents)
        {
            Meeting testMeeting = new Meeting();
            testMeeting.ConnectedUsers = new List<User>();
            for(int currentUser = 0; currentUser < numberofUsers; currentUser++)
            {
                User testUser = new User()
                {
                    UserId = currentUser,
                    Calendar = GenerateCalendar(numberofEvents)
                };
                testMeeting.ConnectedUsers.Add(testUser);
            }

            return testMeeting;
        }

        public Calendar GenerateCalendar(int numberofEvents)
        {
            Calendar testCalendar = new Calendar
            {
                UserCalendar = new List<CalendarEvent>()
            };

            CalendarEvent calendarEvent = new CalendarEvent
            {
                EventStart = DateTime.Now,
                EventEnd = DateTime.Now
            };

            for (int generateNextEvent=0; generateNextEvent < numberofEvents; generateNextEvent++)
            {
                calendarEvent = GenerateCalendarEvent(calendarEvent);
                testCalendar.UserCalendar.Add(calendarEvent);
            }

            return testCalendar;
        }

        private CalendarEvent GenerateCalendarEvent(CalendarEvent prevEvent)
        {
            var prevEventTimeEnd = prevEvent.EventEnd;
            var time = (prevEventTimeEnd - DateTime.MinValue).TotalSeconds;
            var timePlusInterval = (prevEventTimeEnd - DateTime.MinValue + new TimeSpan(0, 2, 0, 0)).TotalSeconds;

            var rnd = new Random();

            TimeSpan nextEventInsertedTimeSpan = TimeSpan.FromSeconds(time + (timePlusInterval - time) * rnd.NextDouble());
            DateTime nextEventTimeStart = DateTime.MinValue.Add(nextEventInsertedTimeSpan);
            var nextEventTimeEnd = nextEventTimeStart.AddHours(rnd.Next(0,3)).AddMinutes(rnd.Next(0, 59));

            return new CalendarEvent
            {
                EventStart = nextEventTimeStart,
                EventEnd = nextEventTimeEnd
            };

        }
    }
}