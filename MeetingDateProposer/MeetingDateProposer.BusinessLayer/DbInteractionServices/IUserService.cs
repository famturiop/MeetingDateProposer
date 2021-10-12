using System;
using MeetingDateProposer.Domain.Models;
using MeetingDateProposer.Domain.Models.ApplicationModels;

namespace MeetingDateProposer.BusinessLayer.DbInteractionServices
{
    public interface IUserService
    {
        public void AddUserToDb(ApplicationUser user);

        public void RemoveUserFromDb(Guid id);

        public ApplicationUser GetUserByIdFromDb(Guid id);

    }
}
