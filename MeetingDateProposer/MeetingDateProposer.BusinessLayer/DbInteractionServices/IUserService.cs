using MeetingDateProposer.Domain.Models.ApplicationModels;
using System;
using System.Threading.Tasks;

namespace MeetingDateProposer.BusinessLayer.DbInteractionServices
{
    public interface IUserService
    {
        public Task CreateUserAsync(ApplicationUser user);

        public Task DeleteUserAsync(Guid id);

        public Task<ApplicationUser> GetUserAsync(Guid id);

        public Task AddCalendarToUserAsync(ApplicationUser user, Calendar calendar);

    }
}
