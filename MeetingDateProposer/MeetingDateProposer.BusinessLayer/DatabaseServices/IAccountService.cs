using System;
using MeetingDateProposer.Domain.Models;
using MeetingDateProposer.Domain.Models.AccountViewModels;

namespace MeetingDateProposer.BusinessLayer.DatabaseServices
{
    public interface IAccountService
    {
        public void AddUserToDb(ApplicationUser user);

        public AccountUser RemoveUserFromDb(Guid id);

        public AccountUser GetUserByIdFromDb(Guid id);

    }
}