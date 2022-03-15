using System;
using System.Collections.Generic;

namespace MeetingDateProposer.Domain.Models.ApplicationModels
{
    public class Meeting
    {
        public Guid Id { get; set; }

        public List<ApplicationUser> ConnectedUsers { get; set; }

        public string Name { get; set; }
    }
}