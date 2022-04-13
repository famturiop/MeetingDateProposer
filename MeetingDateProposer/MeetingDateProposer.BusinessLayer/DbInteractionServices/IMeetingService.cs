using MeetingDateProposer.Domain.Models.ApplicationModels;
using System;
using System.Threading.Tasks;

namespace MeetingDateProposer.BusinessLayer.DbInteractionServices
{
    public interface IMeetingService
    {
        public Task CreateMeetingAsync(Meeting meeting);

        public Task<Meeting> GetMeetingAsync(Guid id);

        public Task AddUserToMeetingAsync(ApplicationUser user, Meeting meeting);

        public Task DeleteMeetingAsync(Guid id);
    }
}