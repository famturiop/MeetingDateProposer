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
            return _appContext.Meetings
                .Include(m => m.ConnectedUsers)
                .ThenInclude(c => c.Calendars)
                .ThenInclude(ce => ce.UserCalendar)
                .FirstOrDefault(m => m.Id == id);
        }

        public void AddUserToMeeting(ApplicationUser user, Meeting meeting)
        {
            _appContext.Meetings.Update(meeting);
            if (meeting.ConnectedUsers == null)
            {
                meeting.ConnectedUsers = new List<ApplicationUser> { user };
            }
            else
            {
                meeting.ConnectedUsers.Add(user);
            }
            _appContext.SaveChanges();
        }

        public void DeleteMeeting(Guid id)
        {
            var meeting = _appContext.Meetings.FirstOrDefault(m => m.Id == id);
            if (meeting != null)
            {
                _appContext.Meetings.Remove(meeting);
                _appContext.SaveChanges();
            }
        }
    }
}