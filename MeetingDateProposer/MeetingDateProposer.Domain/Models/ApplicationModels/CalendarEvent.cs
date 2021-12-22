using System;
using System.ComponentModel.DataAnnotations;

namespace MeetingDateProposer.Domain.Models.ApplicationModels
{
    public class CalendarEvent
    {
        public int Id { get; set; }

        public int CalendarId { get; set; }

        public Calendar Calendar { get; set; }

        public DateTime EventStart { get; set; }

        public DateTime EventEnd { get; set; }
    }
}