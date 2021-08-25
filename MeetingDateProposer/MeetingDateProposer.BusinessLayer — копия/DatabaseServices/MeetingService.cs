using System;
using System.Collections.Generic;
using System.Linq;
using MeetingDateProposer.DataLayer;
using MeetingDateProposer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingDateProposer.BusinessLayer.DatabaseServices
{
    public class MeetingService: IMeetingService
    {
        private readonly ApplicationContext _appContext;
        public MeetingService(ApplicationContext applicationContext)
        {
            _appContext = applicationContext;
        }
        public void AddMeetingToDb(Meeting meeting)
        {
            _appContext.Meetings.Add(meeting);
            _appContext.SaveChanges();
        }

        public Meeting GetMeetingbyIdFromDb(Guid id)
        {
            if (_appContext.Meetings.Where(u => u.Id == id).Any())
            {
                return _appContext.Meetings.Include(m => m.ConnectedUsers)
                    .ThenInclude(c => c.Calendars)
                    .ThenInclude(ce => ce.UserCalendar)
                    .Where(u => u.Id == id).First();
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
    }
}