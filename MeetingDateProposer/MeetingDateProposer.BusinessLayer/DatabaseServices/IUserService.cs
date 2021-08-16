using System;
using MeetingDateProposer.Domain.Models;

namespace MeetingDateProposer.BusinessLayer.DatabaseServices
{
    public interface IUserService
    {
        public void AddUserToDb(User user);

        public void RemoveUserFromDb(User user);

        public User GetUserbyIdFromDb(Guid Id);

    }
}
