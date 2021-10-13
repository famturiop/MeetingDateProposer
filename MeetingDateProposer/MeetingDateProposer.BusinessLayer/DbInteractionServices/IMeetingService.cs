using System;
using System.Threading.Tasks;
using MeetingDateProposer.Domain.Models;
using MeetingDateProposer.Domain.Models.ApplicationModels;

namespace MeetingDateProposer.BusinessLayer.DbInteractionServices
{
    public interface IMeetingService
    {
        public Task AddMeetingToDbAsync(Meeting meeting);
        public Task<Meeting> GetMeetingByIdFromDbAsync(Guid id);
        public Task AddUserToMeetingAsync(ApplicationUser user, Meeting meeting);
        public Task DeleteMeetingAsync(Guid id);
    }
}