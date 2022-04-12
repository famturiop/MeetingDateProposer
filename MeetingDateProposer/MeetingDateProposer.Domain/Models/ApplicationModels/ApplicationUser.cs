using MeetingDateProposer.Domain.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public override bool Equals(object obj)
        {
            var user = obj as ApplicationUser;
            if (user == null)
                return false;
            return user.Name == Name && 
                   user.Calendars.TrueForAll(x => Calendars.Any(y => x.Equals(y)));
        }

        public override int GetHashCode()
        {
            int calendarsHash = 0;
            Calendars.ForEach(c => calendarsHash += c.GetHashCode());
            return Name.GetHashCode() * calendarsHash;
        }
    }
}