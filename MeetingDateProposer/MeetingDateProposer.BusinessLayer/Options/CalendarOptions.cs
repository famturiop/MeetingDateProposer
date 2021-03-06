namespace MeetingDateProposer.BusinessLayer.Options;

public class CalendarOptions
{
    public const string GoogleCalendar = "Calendars:GoogleCalendar";

    public string ProjectId { get; set; } = string.Empty;

    public string RedirectUri { get; set; } = string.Empty;

    public string ClientId { get; set; } = string.Empty;

    public string ClientSecret { get; set; } = string.Empty;

}