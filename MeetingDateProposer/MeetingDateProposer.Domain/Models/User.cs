namespace MeetingDateProposer.Domain.Models
{
    public class User
    {
        public object Credentials { get; set; }
        public Calendar Calendar { get; set; }
        public int UserId { get; set; }
    }
}