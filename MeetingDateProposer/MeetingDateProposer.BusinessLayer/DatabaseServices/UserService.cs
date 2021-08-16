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

        public void RemoveUserFromDb(User user)
        {
            _appContext.Users.Remove(user);
            _appContext.SaveChanges();
        }

        public User GetUserbyIdFromDb(Guid id)
        {
            if (_appContext.Users.Where(u => u.Id == id).Any())
            {
                return _appContext.Users
                    .Include(u => u.Calendars)
                    .Where(u => u.Id == id).First();
            }
            else
            {
                return null;
            }
            
        }

        public void WipedataTmp()
        {
            var all = from c in _appContext.Users select c;
            _appContext.Users.RemoveRange(all);
            var all2 = from c in _appContext.Meetings select c;
            _appContext.Meetings.RemoveRange(all2);
            _appContext.SaveChanges();
        }
    }
}
