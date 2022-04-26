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
using Microsoft.Extensions.Options;
using Calendar = MeetingDateProposer.Domain.Models.ApplicationModels.Calendar;

namespace MeetingDateProposer.BusinessLayer.Providers
{
    public class GoogleCalendarProvider : ICalendarProvider
    {
        private readonly ICalendarEventFormatter<IList<Event>> _calendarEventFormatter;
        private readonly string _redirectUri;
        private readonly string _projectId;
        private readonly GoogleAuthorizationCodeFlow.Initializer _authorizationCodeFlowInitializer;

        public GoogleCalendarProvider(
            ICalendarEventFormatter<IList<Event>> calendarEventFormatter,
            IOptions<ApiKeysOptions> apiKeysOptions,
            IOptions<CalendarOptions> calendarOptions)
        {
            _calendarEventFormatter = calendarEventFormatter;
            _redirectUri = calendarOptions.Value.RedirectUri;
            _projectId = calendarOptions.Value.ProjectId;

            _authorizationCodeFlowInitializer = new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = apiKeysOptions.Value.ClientId,
                    ClientSecret = apiKeysOptions.Value.ClientSecret
                },
                Scopes = new[]
                {
                    CalendarService.Scope.CalendarReadonly
                },
                ProjectId = calendarOptions.Value.ProjectId
            };
        }

        public Uri GetAuthorizationCodeRequest(Guid userId)
        {
            _authorizationCodeFlowInitializer.UserDefinedQueryParams = new[]
            {
                new KeyValuePair<string, string>("state", userId.ToString())
            };

            using var authorizationCodeFlow = new GoogleAuthorizationCodeFlow(_authorizationCodeFlowInitializer);

            return authorizationCodeFlow.CreateAuthorizationCodeRequest(_redirectUri).Build();

        }

        public async Task<Calendar> GetCalendarAsync(string authorizationCode, Guid userId)
        {
            using var authorizationCodeFlow = new GoogleAuthorizationCodeFlow(_authorizationCodeFlowInitializer);
            var credential = await ExchangeCodeForTokenAsync(authorizationCodeFlow, authorizationCode, userId);
            var events = await GetCalendarEventsAsync(credential);
            
            var calendar = new Calendar
            {
                UserCalendar = _calendarEventFormatter.FormatCalendarEvents(events.Items)
            };

            return calendar;
        }

        private async Task<UserCredential> ExchangeCodeForTokenAsync(
            GoogleAuthorizationCodeFlow authorizationCodeFlow,
            string authorizationCode, 
            Guid userId)
        {
            var tokenResponse = await authorizationCodeFlow.ExchangeCodeForTokenAsync(
                userId.ToString(),
                authorizationCode,
                _redirectUri,
                CancellationToken.None);

            return new UserCredential(authorizationCodeFlow, userId.ToString(), tokenResponse);
        }

        private async Task<Events> GetCalendarEventsAsync(UserCredential credential)
        {
            using var service = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = _projectId
            });

            var request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 250;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            return await request.ExecuteAsync();
        }

    }
}