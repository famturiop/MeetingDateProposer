using System.Collections.Generic;

namespace MeetingDateProposer.Models.ApplicationApiModels
{
    public class CalendarApiModel
    {
        public int Id { get; set; }

        public List<CalendarEventApiModel> UserCalendar { get; set; }
    }
}