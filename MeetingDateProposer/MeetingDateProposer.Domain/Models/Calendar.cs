using System.Collections.Generic;

namespace MeetingDateProposer.Domain.Models
{
    public class Calendar
    {
        public int Id { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
        
        public List<CalendarEvent> UserCalendar { get; set; }
    }
}