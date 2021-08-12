using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MeetingDateProposer.Domain.Models;
using MeetingDateProposer.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace MeetingDateProposer.BusinessLayer.Providers
{
    public class UserProvider: IUserProvider
    {
        private readonly ApplicationContext _appContext;

        public UserProvider(ApplicationContext applicationContext)
        {
            _appContext = applicationContext;
        }

        public void AddUserToDb(User user)
        {
            _appContext.Users.Add(user);
            _appContext.SaveChanges();
        }

        public void RemoveUserFromDb(User user)
        {
            _appContext.Users.Remove(user);
            _appContext.SaveChanges();
        }

        public User GetUserbyIdFromDb(int id)
        {
            if (_appContext.Users.Where(u => u.Id == id).Any())
            {
                return _appContext.Users.Include(u => u.Calendars).Where(u => u.Id == id).First();
            }
            else
            {
                return null;
            }
            
        }

        public Calendar GetCalendarbyIdFromDb(int id)
        {
            if (_appContext.Calendars.Where(u => u.Id == id).Any())
            {
                return _appContext.Calendars.Find(id);
            }
            else
            {
                return null;
            }

        }

        public void AddMeetingToDb(Meeting meeting)
        {
            _appContext.Meetings.Add(meeting);
            _appContext.SaveChanges();
        }

        public Meeting GetMeetingbyIdFromDb(int id)
        {
            if (_appContext.Meetings.Where(u => u.Id == id).Any())
            {
                return _appContext.Meetings.Where(u => u.Id == id).First();
            }
            else
            {
                return null;
            }
        }

        public void AddUserToMeeting(User user, Meeting meeting)
        {
            _appContext.Meetings.Update(meeting);
            if (meeting.ConnectedUsers == null)
            {
                meeting.ConnectedUsers = new List<User> { user };
            }
            else { meeting.ConnectedUsers.Add(user); }
            _appContext.SaveChanges();
        }

        public void WipedataTmp()
        {
            var all = from c in _appContext.Users select c;
            _appContext.Users.RemoveRange(all);
            _appContext.SaveChanges();
        }
    }
}
