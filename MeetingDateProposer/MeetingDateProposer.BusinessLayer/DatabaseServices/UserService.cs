using System;
using System.Linq;
using MeetingDateProposer.DataLayer;
using MeetingDateProposer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingDateProposer.BusinessLayer.DatabaseServices
{
    public class UserService: IUserService
    {
        private readonly ApplicationContext _appContext;

        public UserService(ApplicationContext applicationContext)
        {
            _appContext = applicationContext;
        }

        public void AddUserToDb(User user)
        {
            _appContext.Users.Add(user);
            _appContext.SaveChanges();
        }

        public User RemoveUserFromDb(Guid id)
        {
            if (_appContext.Users.Any(u => u.Id == id))
            {
                var user = _appContext.Users.First(u => u.Id == id);
                _appContext.Users.Remove(user);
                _appContext.SaveChanges();
                return user;
            }
            else
            {
                return null;
            }
        }

        public User GetUserByIdFromDb(Guid id)
        {
            if (_appContext.Users.Any(u => u.Id == id))
            {
                return _appContext.Users
                    .Include(u => u.Calendars)
                    .First(u => u.Id == id);
            }
            else
            {
                return null;
            }
        }
    }
}
