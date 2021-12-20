using System;
using System.ComponentModel.DataAnnotations;

namespace MeetingDateProposer.Models.ApplicationApiModels
{
    public class CalendarEventApiModel
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Start { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime End { get; set; }

        public String Title { get; set; } = "";

        public String Color { get; set; } = "";
    }
}