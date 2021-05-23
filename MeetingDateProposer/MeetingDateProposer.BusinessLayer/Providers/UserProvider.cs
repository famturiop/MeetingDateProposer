using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MeetingDateProposer.Domain.Models;
using MeetingDateProposer.DataLayer;

namespace MeetingDateProposer.BusinessLayer.Providers
{
    public class UserProvider
    {
        private readonly ApplicationContext appContext;

        public UserProvider()
        {
            appContext = new ApplicationContext();
        }

        public void AddUserToDb(User user)
        {
            appContext.Users.Add(user);
            appContext.SaveChanges();
        }

        public void RemoveUserFromDb(User user)
        {
            appContext.Users.Remove(user);
            appContext.SaveChanges();
        }

        public User GetUserbyIdFromDb(int Id)
        {
            return appContext.Users.Where(u => u.Id == Id).First();
        }

    }
}
