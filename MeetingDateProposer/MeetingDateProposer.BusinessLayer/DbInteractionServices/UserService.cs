using MeetingDateProposer.DataLayer;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingDateProposer.BusinessLayer.DbInteractionServices
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _appContext;

        public UserService(ApplicationContext applicationContext)
        {
            _appContext = applicationContext;
        }

        public async Task CreateUserAsync(ApplicationUser user)
        {
            await _appContext.ApplicationUsers.AddAsync(user);
            await _appContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _appContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                _appContext.ApplicationUsers.Remove(user);
                await _appContext.SaveChangesAsync();
            }
        }

        public Task<ApplicationUser> GetUserAsync(Guid id)
        {
            return _appContext.ApplicationUsers
                .Include(u => u.Calendars)
                .ThenInclude(ce => ce.UserCalendar)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddCalendarToUserAsync(ApplicationUser user, Calendar calendar)
        {
            if (user.Calendars == null)
            {
                user.Calendars = new List<Calendar> { calendar };
            }
            else
            {
                user.Calendars.Add(calendar);
            }

            _appContext.ApplicationUsers.Update(user);
            await _appContext.SaveChangesAsync();
        }
    }
}
