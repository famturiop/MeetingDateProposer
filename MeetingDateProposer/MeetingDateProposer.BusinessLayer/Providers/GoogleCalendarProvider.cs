using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MeetingDateProposer.Domain.Models;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using Calendar = MeetingDateProposer.Domain.Models.ApplicationModels.Calendar;

namespace MeetingDateProposer.BusinessLayer.Providers
{
    public class GoogleCalendarProvider : ICalendarProvider
    {
        public void GetCalendar(ApplicationUser user)
        {
            string[] Scopes = {CalendarService.Scope.CalendarReadonly};
            var credential = GetAccessToGoogle(Scopes, user); 
            var events = SendRequestToGoogle(credential);

            var calendar = new Calendar
            {
                UserCalendar = new List<CalendarEvent>()
            };

            foreach (var eventItem in events.Items)
            {
                calendar.UserCalendar.Add(new CalendarEvent
                {
                    EventStart = (DateTime)eventItem.Start.DateTime,
                    EventEnd = (DateTime)eventItem.End.DateTime
                });
            }

            if (user.Calendars == null)
            {
                user.Calendars = new List<Calendar> {calendar};
            }
            else { user.Calendars.Add(calendar); }
            

        }

        private UserCredential GetAccessToGoogle(string[] Scopes, ApplicationUser user)
        {
            UserCredential credential;
            LocalServerCodeReceiver redirectURI = new LocalServerCodeReceiver("You may close the page now.",
                LocalServerCodeReceiver.CallbackUriChooserStrategy.Default);
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                return credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    user.Id.ToString(),
                    CancellationToken.None,
                    new NullDataStore(), redirectURI).Result;
            }
        }

        private Events SendRequestToGoogle(UserCredential credential)
        {
            var service = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "My Project 88493"
            });

            var request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 250;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            var events = request.Execute();
            return events;
        }

        private void NullEventsHandler()
        {

        }
    }
}