namespace MeetingDateProposer.DataLayer.Options
{
    public class SeededUsersOptions
    {
        public const string Admin = "SeededUsers:Admin";

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

    }
}
