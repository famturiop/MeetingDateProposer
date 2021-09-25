using System;
using MeetingDateProposer.Domain.Models;
using MeetingDateProposer.Domain.Models.AccountModels;

namespace MeetingDateProposer.BusinessLayer.DbInteractionServices
{
    public interface IAccountService
    {
        public void AddUserToDb(ApplicationUser user);

        public AccountUser RemoveUserFromDb(Guid id);

        public AccountUser GetUserByIdFromDb(Guid id);

    }
}