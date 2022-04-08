using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2.Flows;
using MeetingDateProposer.BusinessLayer.Formatters;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Calendar = MeetingDateProposer.Domain.Models.ApplicationModels.Calendar;

namespace MeetingDateProposer.BusinessLayer.Providers
{
    public class GoogleCalendarProvider : ICalendarProvider
    {
        private readonly ICalendarEventFormatter<IList<Event>> _calendarEventFormatter;
        private readonly IConfiguration _configuration;

        public GoogleCalendarProvider(
            ICalendarEventFormatter<IList<Event>> calendarEventFormatter,
            IConfiguration configuration)
        {
            _calendarEventFormatter = calendarEventFormatter;
            _configuration = configuration;
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
            var clientId = _configuration["googleCredentials:client_id"];
            var clientSecret = _configuration["googleCredentials:client_secret"];
            var redirectUri = _configuration["googleCredentials:redirect_uris"];

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
                ApplicationName = _configuration["googleCredentials:project_id"]
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