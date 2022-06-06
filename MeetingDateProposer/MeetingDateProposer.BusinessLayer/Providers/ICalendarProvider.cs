using MeetingDateProposer.Domain.Models.ApplicationModels;
using System;
using System.Threading.Tasks;

namespace MeetingDateProposer.BusinessLayer.Providers
{
    public interface ICalendarProvider
    {
        public Uri GetAuthorizationCodeRequest(Guid userId);

        public Task<Calendar> GetCalendarAsync(string authorizationCode, Guid userId);
    }
}