using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingDateProposer.Domain.Models.ApplicationModels;

namespace MeetingDateProposer.BusinessLayer.Formatters
{
    public interface ICalendarEventFormatter<T> where T : IEnumerable
    {
        public List<CalendarEvent> FormatCalendarEvents(T events);
    }
}
