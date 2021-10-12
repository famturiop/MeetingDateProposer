using System;
using System.Linq;
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

        public void AddUserToDb(ApplicationUser user)
        {
            _appContext.ApplicationUsers.Add(user);
            _appContext.SaveChanges();
        }

        public void RemoveUserFromDb(Guid id)
        {
            var user = _appContext.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _appContext.ApplicationUsers.Remove(user);
                _appContext.SaveChanges();
            }
        }

        public ApplicationUser GetUserByIdFromDb(Guid id)
        {
            return _appContext.ApplicationUsers
                .Include(u => u.Calendars)
                .FirstOrDefault(u => u.Id == id);
        }
    }
}
