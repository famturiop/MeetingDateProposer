using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetingDateProposer.Domain.Models.ApplicationModels
{
    public class Calendar
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        public Guid UserId { get; set; }

        public List<CalendarEvent> UserCalendar { get; set; }

        public override bool Equals(object obj)
        {
            var calendar = obj as Calendar;
            if (calendar == null)
                return false;
            return calendar.UserCalendar.TrueForAll(x => UserCalendar.Any(y => x.Equals(y)));
        }

        public override int GetHashCode()
        {
            int calendarEventsHash = 0;
            UserCalendar.ForEach(c => calendarEventsHash += c.GetHashCode());
            return calendarEventsHash;
        }
    }
}