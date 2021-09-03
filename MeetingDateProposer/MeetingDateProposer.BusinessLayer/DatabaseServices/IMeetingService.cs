using System;
using MeetingDateProposer.Domain.Models;

namespace MeetingDateProposer.BusinessLayer.DatabaseServices
{
    public interface IMeetingService
    {
        public void AddMeetingToDb(Meeting meeting);
        public Meeting GetMeetingByIdFromDb(Guid id);
        public void AddUserToMeeting(User user, Meeting meeting);
        public Meeting DeleteMeeting(Guid id);
    }
}