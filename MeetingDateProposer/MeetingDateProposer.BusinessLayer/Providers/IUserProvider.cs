using System;
using System.Collections.Generic;
using System.Text;
using MeetingDateProposer.Domain.Models;

namespace MeetingDateProposer.BusinessLayer.Providers
{
    public interface IUserProvider
    {
        public void AddUserToDb(User user);

        public void RemoveUserFromDb(User user);

        public User GetUserbyIdFromDb(int Id);
        public Calendar GetCalendarbyIdFromDb(int Id);
        public void AddMeetingToDb(Meeting meeting);
        public Meeting GetMeetingbyIdFromDb(int id);
        public void AddUserToMeeting(User user, Meeting meeting);
        public void WipedataTmp();
    }
}
