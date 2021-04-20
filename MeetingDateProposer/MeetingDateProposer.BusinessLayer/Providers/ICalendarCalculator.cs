using MeetingDateProposer.Domain.Models;

namespace MeetingDateProposer.BusinessLayer.Providers
{
    public interface ICalendarCalculator
    {
        public Calendar CalculateAvailableMeetingTime(Meeting currentMeeting);
    }
}