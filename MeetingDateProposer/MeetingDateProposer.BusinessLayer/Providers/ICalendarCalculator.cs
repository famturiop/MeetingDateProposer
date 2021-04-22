using System.Collections.Generic;
using System.Linq;
using MeetingDateProposer.DataLayer;
using MeetingDateProposer.Domain.Models;

namespace MeetingDateProposer.BusinessLayer.Providers
{
    public interface ICalendarCalculator
    {
        public Calendar CalculateAvailableMeetingTime(Meeting currentMeeting)
        {
            return new Calendar();
        }
    }
}