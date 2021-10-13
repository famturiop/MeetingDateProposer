using System;
using System.Linq;
using System.Threading.Tasks;
using MeetingDateProposer.DataLayer;
using MeetingDateProposer.Domain.Models;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using Microsoft.EntityFrameworkCore;

namespace MeetingDateProposer.BusinessLayer.DbInteractionServices
{
    public class UserService: IUserService
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
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
