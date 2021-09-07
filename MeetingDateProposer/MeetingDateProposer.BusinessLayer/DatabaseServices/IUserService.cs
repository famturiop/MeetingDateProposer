using System;
using MeetingDateProposer.Domain.Models;

namespace MeetingDateProposer.BusinessLayer.DatabaseServices
{
    public interface IUserService
    {
        public void AddUserToDb(ApplicationUser user);

        public ApplicationUser RemoveUserFromDb(Guid id);

        public ApplicationUser GetUserByIdFromDb(Guid id);

    }
}
