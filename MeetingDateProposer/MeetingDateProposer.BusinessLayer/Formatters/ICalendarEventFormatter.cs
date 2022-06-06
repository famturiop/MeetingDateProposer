using MeetingDateProposer.Domain.Models.ApplicationModels;
using System.Collections;
using System.Collections.Generic;

namespace MeetingDateProposer.BusinessLayer.Formatters
{
    public interface ICalendarEventFormatter<T> where T : IEnumerable
    {
        public List<CalendarEvent> FormatCalendarEvents(T events);
    }
}
