using System;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using MeetingDateProposer.Domain.Models.ApplicationModels;

namespace MeetingDateProposer.BusinessLayer.Providers
{
    public interface ICalendarProvider
    {
        public Task<Calendar> GetCalendarAsync(string authorizationCode, Guid userId);

    }
}