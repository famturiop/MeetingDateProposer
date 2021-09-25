using System;
using MeetingDateProposer.Domain.Models;

namespace MeetingDateProposer.BusinessLayer.DbInteractionServices
{
    public interface IMeetingService
    {
        public void AddMeetingToDb(Meeting meeting);
        public Meeting GetMeetingByIdFromDb(Guid id);
        public void AddUserToMeeting(ApplicationUser user, Meeting meeting);
        public Meeting DeleteMeeting(Guid id);
    }
}