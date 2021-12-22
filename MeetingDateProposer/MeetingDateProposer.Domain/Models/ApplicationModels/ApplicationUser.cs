using MeetingDateProposer.Domain.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeetingDateProposer.Domain.Models.ApplicationModels
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