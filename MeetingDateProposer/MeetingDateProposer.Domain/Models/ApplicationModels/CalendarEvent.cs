using System;

namespace MeetingDateProposer.Domain.Models.ApplicationModels
{
    public class CalendarEvent
    {
        public int Id { get; set; }

        public int CalendarId { get; set; }

        public Calendar Calendar { get; set; }

        public DateTime EventStart { get; set; }

        public DateTime EventEnd { get; set; }

        public override bool Equals(object obj)
        {
            var calendarEvent = obj as CalendarEvent;
            if (calendarEvent == null)
                return false;
            return calendarEvent.EventStart == EventStart &&
                   calendarEvent.EventEnd == EventEnd;
        }

        public override int GetHashCode()
        {
            return EventStart.GetHashCode() * EventEnd.GetHashCode();
        }
    }
}