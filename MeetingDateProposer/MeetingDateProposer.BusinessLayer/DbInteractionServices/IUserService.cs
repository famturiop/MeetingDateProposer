using MeetingDateProposer.Domain.Models.ApplicationModels;
using System;
using System.Threading.Tasks;

namespace MeetingDateProposer.BusinessLayer.DbInteractionServices
{
    public interface IUserService
    {
        public Task AddUserToDbAsync(ApplicationUser user);

        public Task RemoveUserFromDbAsync(Guid id);

        public Task<ApplicationUser> GetUserByIdFromDbAsync(Guid id);

    }
}
