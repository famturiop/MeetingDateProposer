using System.Collections.Generic;

namespace MeetingDateProposer.Domain.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        public List<User> ConnectedUsers { get; set; }
    }
}