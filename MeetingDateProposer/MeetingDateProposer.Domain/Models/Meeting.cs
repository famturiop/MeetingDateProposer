using System;
using System.Collections.Generic;

namespace MeetingDateProposer.Domain.Models
{
    public class Meeting
    {
        public Guid Id { get; set; }
        public List<User> ConnectedUsers { get; set; }
        public string Name { get; set; }
    }
}