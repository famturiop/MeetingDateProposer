using System;
using System.Collections.Generic;

namespace MeetingDateProposer.Domain.Models.ApplicationModels
{
    public class Calendar
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        public Guid UserId { get; set; }
        
        public List<CalendarEvent> UserCalendar { get; set; }
    }
}