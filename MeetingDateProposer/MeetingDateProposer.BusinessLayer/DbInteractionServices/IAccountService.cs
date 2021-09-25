using System;
using MeetingDateProposer.Domain.Models;
using MeetingDateProposer.Domain.Models.AccountModels;
using MeetingDateProposer.Domain.Models.ApplicationModels;

namespace MeetingDateProposer.BusinessLayer.DbInteractionServices
{
    public interface IAccountService
    {
        public void AddUserToDb(ApplicationUser user);

        public AccountUser RemoveUserFromDb(Guid id);

        public AccountUser GetUserByIdFromDb(Guid id);

    }
}