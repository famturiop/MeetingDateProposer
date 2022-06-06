using MeetingDateProposer.Domain.Models.ApplicationModels;
using System.Collections.Generic;

namespace MeetingDateProposer.BusinessLayer
{
    public interface ICalendarCalculator
    {
        public Calendar CalculateAvailableMeetingTime(Meeting meeting);
        public Calendar CalculateAvailableMeetingTime(List<Calendar> calendarsToCompare);
    }
}