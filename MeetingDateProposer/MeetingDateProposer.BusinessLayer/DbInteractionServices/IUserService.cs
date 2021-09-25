using System;
using MeetingDateProposer.Domain.Models;

namespace MeetingDateProposer.BusinessLayer.DbInteractionServices
{
    public interface IUserService
    {
        public void AddUserToDb(ApplicationUser user);

        public ApplicationUser RemoveUserFromDb(Guid id);

        public ApplicationUser GetUserByIdFromDb(Guid id);

    }
}
