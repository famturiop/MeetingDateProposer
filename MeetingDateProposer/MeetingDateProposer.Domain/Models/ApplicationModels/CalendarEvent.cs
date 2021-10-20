using System;
using System.ComponentModel.DataAnnotations;

namespace MeetingDateProposer.Domain.Models.ApplicationModels
{
    public class CalendarEvent
    {
        public int Id { get; set; }

        public int CalendarId { get; set; }

        public Calendar Calendar { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EventStart { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EventEnd { get; set; }
    }
}