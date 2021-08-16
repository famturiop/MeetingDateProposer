using MeetingDateProposer.Domain.Models;

namespace MeetingDateProposer.BusinessLayer
{
    public interface ICalendarCalculator
    {
        public Calendar CalculateAvailableMeetingTime(Meeting currentMeeting);
    }
}