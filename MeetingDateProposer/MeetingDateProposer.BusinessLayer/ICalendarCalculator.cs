using System.Collections.Generic;
using MeetingDateProposer.Domain.Models.ApplicationModels;

namespace MeetingDateProposer.BusinessLayer
{
    public interface ICalendarCalculator
    {
        public Calendar CalculateAvailableMeetingTime(Meeting meeting);
        public Calendar CalculateAvailableMeetingTime(List<Calendar> calendarsToCompare);
    }
}