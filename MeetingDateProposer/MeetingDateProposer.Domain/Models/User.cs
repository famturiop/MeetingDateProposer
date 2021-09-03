using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

namespace MeetingDateProposer.Domain.Models
{
    public class User: IdentityUser<Guid>
    {
        public object Credentials { get; set; }
        public List<Calendar> Calendars { get; set; }
        public List<Meeting> UserMeetings { get; set; }
        public string Name { get; set; }
    }
}