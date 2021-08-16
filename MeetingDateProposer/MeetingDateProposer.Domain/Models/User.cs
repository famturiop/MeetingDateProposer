using System;
using System.Collections.Generic;

namespace MeetingDateProposer.Domain.Models
{
    public class User
    {
        public object Credentials { get; set; }
        public List<Calendar> Calendars { get; set; }
        public List<Meeting> UserMeetings { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}