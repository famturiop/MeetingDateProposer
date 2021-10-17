using MeetingDateProposer.Domain.Models.ApplicationModels;

namespace MeetingDateProposer.BusinessLayer.Providers
{
    public interface ICalendarProvider
    {
        public void GetCalendar(ApplicationUser user);

    }
}