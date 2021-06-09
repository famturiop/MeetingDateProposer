using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MeetingDateProposer.Domain.Models;
using MeetingDateProposer.DataLayer;

namespace MeetingDateProposer.BusinessLayer.Providers
{
    public class UserProvider: IUserProvider
    {
        private readonly ApplicationContext _appContext;

        public UserProvider(ApplicationContext applicationContext)
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

        public User GetUserbyIdFromDb(int Id)
        {
            return _appContext.Users.Where(u => u.Id == Id).First();
        }

    }
}
