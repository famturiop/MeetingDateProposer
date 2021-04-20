namespace MeetingDateProposer.Domain.Models
{
    public class Calendar
    {
        private  object data { get; set; }
    }

    public class User
    { 
        private object login { get; set; }
        private object password { get; set; }
        private object calendar { get; set; }
    }
}