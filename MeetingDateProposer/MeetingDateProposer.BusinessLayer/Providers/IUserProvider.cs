using System;
using System.Collections.Generic;
using System.Text;
using MeetingDateProposer.Domain.Models;

namespace MeetingDateProposer.BusinessLayer.Providers
{
    public interface IUserProvider
    {
        public void AddUserToDb(User user);

        public void RemoveUserFromDb(User user);

        public User GetUserbyIdFromDb(int Id);
    }
}
