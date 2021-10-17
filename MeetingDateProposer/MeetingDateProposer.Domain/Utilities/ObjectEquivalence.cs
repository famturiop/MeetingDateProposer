using MeetingDateProposer.Domain.Models.ApplicationModels;
using System.Linq;

namespace MeetingDateProposer.Domain.Utilities
{
    public class ObjectEquivalence
    {
        public static bool CalendarCheck(Calendar cal1, Calendar cal2)
        {
            return cal1.UserCalendar.All(x => cal2.UserCalendar.Any(y => x.EventStart == y.EventStart && x.EventEnd == y.EventEnd));
        }

        public static bool CalendarEventCheck(CalendarEvent calEvent1, CalendarEvent calEvent2)
        {
            return calEvent1 == calEvent2;
        }
    }
}
