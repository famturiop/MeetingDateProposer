using System;
using System.Collections.Generic;
using MeetingDateProposer.Domain.Models.AccountViewModels;

namespace MeetingDateProposer.Domain.Models
{
    public class ApplicationUser
    {
        public object Credentials { get; set; }
        public List<Calendar> Calendars { get; set; }
        public Guid Id { get; set; }
        public List<Meeting> UserMeetings { get; set; }
        public string Name { get; set; }
        public Guid? AccountUserId { get; set; }
        public AccountUser AccountUser { get; set; }
    }
}