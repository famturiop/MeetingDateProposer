using System;
using System.Collections.Generic;
using System.Linq;
using MeetingDateProposer.DataLayer;
using MeetingDateProposer.Domain.Models;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using Microsoft.EntityFrameworkCore;

namespace MeetingDateProposer.BusinessLayer.DbInteractionServices
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

        public Meeting GetMeetingByIdFromDb(Guid id)
        {
            if (_appContext.Meetings.Any(u => u.Id == id))
            {
                return _appContext.Meetings.Include(m => m.ConnectedUsers)
                    .ThenInclude(c => c.Calendars)
                    .ThenInclude(ce => ce.UserCalendar)
                    .First(u => u.Id == id);
            }
            else
            {
                return null;
            }
        }

        public void AddUserToMeeting(ApplicationUser user, Meeting meeting)
        {
            _appContext.Meetings.Update(meeting);
            if (meeting.ConnectedUsers == null)
            {
                meeting.ConnectedUsers = new List<ApplicationUser> { user };
            }
            else { meeting.ConnectedUsers.Add(user); }
            _appContext.SaveChanges();
        }

        public Meeting DeleteMeeting(Guid id)
        {
            if (_appContext.Meetings.Any(m => m.Id == id))
            {
                var meeting = _appContext.Meetings.First(m => m.Id == id);
                _appContext.Meetings.Remove(meeting);
                _appContext.SaveChanges();
                return meeting;
            }
            else
            {
                return null;
            }
        }
    }
}