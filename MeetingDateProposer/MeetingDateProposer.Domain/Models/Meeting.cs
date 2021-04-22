using System.Collections.Generic;

namespace MeetingDateProposer.Domain.Models
{
    public class Meeting
    {
        public object MeetingID { get; set; }
        public List<User> ConnectedUsers { get; set; }
    }
}