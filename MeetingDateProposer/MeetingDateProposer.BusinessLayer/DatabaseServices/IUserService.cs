using System;
using MeetingDateProposer.Domain.Models;

namespace MeetingDateProposer.BusinessLayer.DatabaseServices
{
    public interface IUserService
    {
        public void AddUserToDb(User user);

        public User RemoveUserFromDb(Guid id);

        public User GetUserByIdFromDb(Guid id);

    }
}
