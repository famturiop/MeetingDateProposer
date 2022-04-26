namespace MeetingDateProposer.BusinessLayer.Options
{
    public class ApiKeysOptions
    {
        public const string GoogleCalendarApiKeys = "ApiKeys:GoogleCalendar";

        public string ClientId { get; set; } = string.Empty;

        public string ClientSecret { get; set; } = string.Empty;

    }
}
