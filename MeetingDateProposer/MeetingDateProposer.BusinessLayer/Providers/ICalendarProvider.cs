using MeetingDateProposer.Domain.Models;

namespace MeetingDateProposer.BusinessLayer.Providers
{
    public  interface ICalendarProvider
    {
        public void GetCalendar(User user);

    }
}