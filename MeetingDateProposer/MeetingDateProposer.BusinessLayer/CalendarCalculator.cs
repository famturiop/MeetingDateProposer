using System;
using System.Collections.Generic;
using System.Linq;
using MeetingDateProposer.Domain.Models;
using MeetingDateProposer.Domain.Models.ApplicationModels;

namespace MeetingDateProposer.BusinessLayer
{
    public class CalendarCalculator: ICalendarCalculator
    {
        
        public Calendar CalculateAvailableMeetingTime(Meeting currentMeeting)
        {
            var jointCalendar = new Calendar
            {
                UserCalendar = new List<CalendarEvent>()
            };

            currentMeeting.ConnectedUsers.ForEach(c1 => c1.Calendars.ForEach(c2 => jointCalendar.UserCalendar.AddRange(c2.UserCalendar)));
            

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
            else
            {
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
        }
        private static List<CalendarEvent> NullCheck(List<CalendarEvent> calendarList)
        {
            if (calendarList.Any(c => c.EventStart == null || c.EventEnd == null))
            {
                throw new Exception("A null Event time was detected.");
            }

            Action<CalendarEvent> CalEventInsertedDelegate = (calEvnt) => 
            {
                calEvnt.EventStart = (DateTime)calEvnt.EventStart;
                calEvnt.EventEnd = (DateTime)calEvnt.EventEnd;
            };

            calendarList.ForEach(CalEventInsertedDelegate);
            return calendarList;
        }

        private static void ReversedCalEventsCheck(List<CalendarEvent> calendarList)
        {
            if (calendarList.Any(x => x.EventStart > x.EventEnd))
            {
                throw new Exception("An event with start time later than end time was detected.");
            }
        }

        private static List<CalendarEvent> RemoveDuplicateCalEvents(List<CalendarEvent> calendarList)
        {
            return calendarList.GroupBy(x => new { x.EventStart, x.EventEnd }).Select(x => x.First()).ToList();
        }

        private static List<CalendarEvent> RemoveNestedCalEvents(List<CalendarEvent> calendarList)
        {
            return calendarList.Where(x => !calendarList.Any(
                y => x.EventStart >= y.EventStart && x.EventEnd <= y.EventEnd && !x.Equals(y))).ToList();
        }

        private static List<CalendarEvent> UniteIntersectedCalEvents(List<CalendarEvent> calendarList)
        {
            var IntersectedEventIndex = calendarList.FindIndex(x => calendarList.Any(
                y => x.EventStart >= y.EventStart && x.EventStart <= y.EventEnd && !x.Equals(y)));
            while (IntersectedEventIndex != -1)
            {
                var UnitedCalEvent = new CalendarEvent
                {
                    EventStart = calendarList[IntersectedEventIndex - 1].EventStart, 
                    EventEnd = calendarList[IntersectedEventIndex].EventEnd
                };

                calendarList.RemoveAt(IntersectedEventIndex);
                calendarList.RemoveAt(IntersectedEventIndex - 1);
                calendarList.Add(UnitedCalEvent);
                calendarList.Sort((x, y) => DateTime.Compare(x.EventStart, y.EventStart));

                IntersectedEventIndex = calendarList.FindIndex(x => calendarList.Any(
                    y => x.EventStart >= y.EventStart && x.EventStart <= y.EventEnd && !x.Equals(y)));
            }
            return calendarList;
        }

        private static List<CalendarEvent> GetAvailableSchedule(List<CalendarEvent> calendarList)
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
