using MeetingDateProposer.Domain.Models.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetingDateProposer.BusinessLayer
{
    public class CalendarCalculator : ICalendarCalculator
    {
        public Calendar CalculateAvailableMeetingTime(Meeting meeting)
        {
            var usersCalendars = new List<Calendar>();
            foreach (var user in meeting.ConnectedUsers)
            {
                usersCalendars.AddRange(user.Calendars);
            }

            return CalculateAvailableMeetingTime(usersCalendars);
        }

        public Calendar CalculateAvailableMeetingTime(List<Calendar> calendarsToCompare)
        {
            var jointCalendar = new Calendar
            {
                UserCalendar = new List<CalendarEvent>()
            };

            foreach (var calendar in calendarsToCompare)
            {
                jointCalendar.UserCalendar.AddRange(calendar.UserCalendar);
            }

            if (jointCalendar.UserCalendar.Count != 0)
            {
                ReversedCalEventsCheck(jointCalendar.UserCalendar);
                
                jointCalendar.UserCalendar.Sort((x, y) => DateTime.Compare(x.EventStart, y.EventStart));

                var jCalRemoveDuplicates = RemoveDuplicateCalEvents(jointCalendar.UserCalendar);
                var jCalNoNestedEvents = RemoveNestedCalEvents(jCalRemoveDuplicates);
                var jCalNoIntersectedEvents = UniteIntersectedCalEvents(jCalNoNestedEvents);
                var jCalInversed = GetAvailableSchedule(jCalNoIntersectedEvents);
                
                return new Calendar()
                {
                    UserCalendar = jCalInversed
                };
            }

            return new Calendar()
            {
                UserCalendar = new List<CalendarEvent>
                {
                    new CalendarEvent()
                    {
                        EventStart = DateTime.MinValue,
                        EventEnd = DateTime.MaxValue
                    }
                }
            };
        }

        private static void ReversedCalEventsCheck(List<CalendarEvent> calendarList)
        {
            if (calendarList.Any(x => x.EventStart > x.EventEnd))
            {
                throw new ArgumentException("An event with start time later than end time was detected.");
            }
        }

        private static List<CalendarEvent> RemoveDuplicateCalEvents(List<CalendarEvent> calendarList)
        {
            return calendarList
                .GroupBy(x => new { x.EventStart, x.EventEnd })
                .Select(x => x.First()).ToList();
        }

        private static List<CalendarEvent> RemoveNestedCalEvents(List<CalendarEvent> calendarList)
        {
            return calendarList
                .Where(x => !calendarList.Any(
                    y => x.EventStart >= y.EventStart && x.EventEnd <= y.EventEnd && !x.Equals(y)))
                .ToList();
        }

        private static List<CalendarEvent> UniteIntersectedCalEvents(List<CalendarEvent> calendarList)
        {
            var intersectedEventIndex = calendarList.FindIndex(x => calendarList.Any(
                y => x.EventStart >= y.EventStart && x.EventStart <= y.EventEnd && !x.Equals(y)));
            
            while (intersectedEventIndex != -1)
            {
                var unitedCalEvent = new CalendarEvent
                {
                    EventStart = calendarList[intersectedEventIndex - 1].EventStart,
                    EventEnd = calendarList[intersectedEventIndex].EventEnd
                };

                calendarList.RemoveAt(intersectedEventIndex);
                calendarList.RemoveAt(intersectedEventIndex - 1);
                calendarList.Add(unitedCalEvent);
                calendarList.Sort((x, y) => DateTime.Compare(x.EventStart, y.EventStart));

                intersectedEventIndex = calendarList.FindIndex(x => calendarList.Any(
                    y => x.EventStart >= y.EventStart && x.EventStart <= y.EventEnd && !x.Equals(y)));
            }
            
            return calendarList;
        }

        private static List<CalendarEvent> GetAvailableSchedule(
            List<CalendarEvent> calendarList)
        {
            var calendarListModified = new List<CalendarEvent>();

            if (calendarList.First().EventStart != DateTime.MinValue)
            {
                calendarListModified.Add(new CalendarEvent()
                {
                    EventStart = DateTime.MinValue,
                    EventEnd = calendarList.First().EventStart
                });
            }

            for (int i = 0; i < calendarList.Count - 1; i++)
            {
                calendarListModified.Add(new CalendarEvent
                {
                    EventStart = calendarList[i].EventEnd,
                    EventEnd = calendarList[i + 1].EventStart
                });
            }

            if (calendarList.Last().EventEnd != DateTime.MaxValue)
            {
                calendarListModified.Add(new CalendarEvent()
                {
                    EventStart = calendarList.Last().EventEnd,
                    EventEnd = DateTime.MaxValue
                });
            }
            
            return calendarListModified;
        }
    }
}
