using MeetingDateProposer.DataLayer;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task AddUserToDbAsync(ApplicationUser user)
        {
            await _appContext.ApplicationUsers.AddAsync(user);
            await _appContext.SaveChangesAsync();
        }

        public async Task RemoveUserFromDbAsync(Guid id)
        {
            var user = await _appContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == id);
            
            if (user != null)
            {
                _appContext.ApplicationUsers.Remove(user);
                await _appContext.SaveChangesAsync();
            }
        }

        public Task<ApplicationUser> GetUserByIdFromDbAsync(Guid id)
        {
            return _appContext.ApplicationUsers
                .Include(u => u.Calendars)
                .ThenInclude(ce => ce.UserCalendar)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateUserAsync(Guid id)
        {
            var user = await _appContext.ApplicationUsers
                .Include(u => u.Calendars)
                .ThenInclude(ce => ce.UserCalendar)
                .FirstOrDefaultAsync(u => u.Id == id);
            _appContext.ApplicationUsers.Update(user);
            await _appContext.SaveChangesAsync();
        }
    }
}
