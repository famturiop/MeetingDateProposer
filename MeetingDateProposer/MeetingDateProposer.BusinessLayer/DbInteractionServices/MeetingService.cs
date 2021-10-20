using MeetingDateProposer.DataLayer;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingDateProposer.BusinessLayer.DbInteractionServices
{
    public class MeetingService : IMeetingService
    {
        private readonly ApplicationContext _appContext;
        public MeetingService(ApplicationContext applicationContext)
        {
            _appContext = applicationContext;
        }

        public async Task AddMeetingToDbAsync(Meeting meeting)
        {
            await _appContext.Meetings.AddAsync(meeting);
            await _appContext.SaveChangesAsync();
        }

        public Task<Meeting> GetMeetingByIdFromDbAsync(Guid id)
        {
            return _appContext.Meetings
                .Include(m => m.ConnectedUsers)
                .ThenInclude(c => c.Calendars)
                .ThenInclude(ce => ce.UserCalendar)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddUserToMeetingAsync(ApplicationUser user, Meeting meeting)
        {

            if (meeting.ConnectedUsers == null)
            {
                meeting.ConnectedUsers = new List<ApplicationUser> { user };
            }
            else
            {
                meeting.ConnectedUsers.Add(user);
            }

            _appContext.Meetings.Update(meeting);
            await _appContext.SaveChangesAsync();
        }

        public async Task DeleteMeetingAsync(Guid id)
        {
            var meeting = await _appContext.Meetings.FirstOrDefaultAsync(m => m.Id == id);

            if (meeting != null)
            {
                _appContext.Meetings.Remove(meeting);
                await _appContext.SaveChangesAsync();
            }
        }
    }
}