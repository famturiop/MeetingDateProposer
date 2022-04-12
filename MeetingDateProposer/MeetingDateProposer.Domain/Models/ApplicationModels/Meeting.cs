using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetingDateProposer.Domain.Models.ApplicationModels
{
    public class Meeting
    {
        public Guid Id { get; set; }

        public List<ApplicationUser> ConnectedUsers { get; set; }

        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var meeting = obj as Meeting;
            if (meeting == null)
                return false;
            return meeting.Name == Name &&
                   meeting.ConnectedUsers.TrueForAll(x => ConnectedUsers.Any(y => x.Equals(y)));
        }

        public override int GetHashCode()
        {
            int usersHash = 0;
            ConnectedUsers.ForEach(c => usersHash += c.GetHashCode());
            return Name.GetHashCode() * usersHash;
        }
    }
}