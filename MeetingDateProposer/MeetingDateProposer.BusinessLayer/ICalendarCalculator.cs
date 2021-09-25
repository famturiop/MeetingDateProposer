using MeetingDateProposer.Domain.Models;
using MeetingDateProposer.Domain.Models.ApplicationModels;

namespace MeetingDateProposer.BusinessLayer
{
    public interface ICalendarCalculator
    {
        public Calendar CalculateAvailableMeetingTime(Meeting currentMeeting);
    }
}