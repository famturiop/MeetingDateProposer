using System;
using System.Threading.Tasks;
using MeetingDateProposer.Domain.Models;
using MeetingDateProposer.Domain.Models.ApplicationModels;

namespace MeetingDateProposer.BusinessLayer.DbInteractionServices
{
    public interface IUserService
    {
        public Task AddUserToDbAsync(ApplicationUser user);

        public Task RemoveUserFromDbAsync(Guid id);

        public Task<ApplicationUser> GetUserByIdFromDbAsync(Guid id);

    }
}
