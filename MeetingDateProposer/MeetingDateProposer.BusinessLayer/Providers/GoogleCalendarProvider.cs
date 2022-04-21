using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2.Flows;
using MeetingDateProposer.BusinessLayer.Formatters;
using MeetingDateProposer.BusinessLayer.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Calendar = MeetingDateProposer.Domain.Models.ApplicationModels.Calendar;

namespace MeetingDateProposer.BusinessLayer.Providers
{
    public class GoogleCalendarProvider : ICalendarProvider
    {
        private readonly ICalendarEventFormatter<IList<Event>> _calendarEventFormatter;
        private readonly IOptions<ApiKeysOptions> _options;

        public GoogleCalendarProvider(
            ICalendarEventFormatter<IList<Event>> calendarEventFormatter,
            IOptions<ApiKeysOptions> options)
        {
            _calendarEventFormatter = calendarEventFormatter;
            _options = options;
        }

        public async Task<Calendar> GetCalendarAsync(string authorizationCode, Guid userId)
        {
            var credential = await ExchangeCodeForTokenAsync(authorizationCode, userId);
            var events = await GetCalendarEventsAsync(credential);
            
            var calendar = new Calendar
            {
                UserCalendar = _calendarEventFormatter.FormatCalendarEvents(events.Items)
            };

            return calendar;
        }

        private async Task<UserCredential> ExchangeCodeForTokenAsync(
            string authorizationCode, 
            Guid userId)
        {
            var clientId = _options.Value.ClientId;
            var clientSecret = _options.Value.ClientSecret;
            var redirectUri = _options.Value.RedirectUri;

            var authorizationCodeFlow = new GoogleAuthorizationCodeFlow(
                new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets()
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                },
                Scopes = new[]
                {
                    CalendarService.Scope.CalendarReadonly
                }
            });

            var tokenResponse = await authorizationCodeFlow.ExchangeCodeForTokenAsync(
                userId.ToString(),
                authorizationCode,
                redirectUri,
                CancellationToken.None);

            return new UserCredential(authorizationCodeFlow, userId.ToString(), tokenResponse);
        }

        private Task<Events> GetCalendarEventsAsync(UserCredential credential)
        {
            var service = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = _options.Value.ProjectId
            });

            var request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 250;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            return request.ExecuteAsync();
        }

    }
}