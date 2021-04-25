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
using Calendar = MeetingDateProposer.Domain.Models.Calendar;

namespace MeetingDateProposer.BusinessLayer.Providers
{
    public class GoogleCalendar : ICalendarProvider
    {
        public void GetCalendar(User user)
        {
            string[] Scopes = {CalendarService.Scope.CalendarReadonly};
            var credential = GetAccessToGoogle(Scopes, user); 
            var events = SendRequestToGoogle(credential);

            var calendar = new Calendar();
            calendar.UserCalendar = new List<CalendarEvent>();

            foreach (var eventItem in events.Items)
            {
                calendar.UserCalendar.Add(new CalendarEvent
                {
                    EventStart = eventItem.Start.DateTime,
                    EventEnd = eventItem.End.DateTime
                });
            }

            user.Calendar = calendar;
        }

        private UserCredential GetAccessToGoogle(string[] Scopes, User user)
        {
            UserCredential credential;
            LocalServerCodeReceiver redirectURI = new LocalServerCodeReceiver("You may close the page now.",
                LocalServerCodeReceiver.CallbackUriChooserStrategy.Default); // redirect URI choice strategy
            // The file token.json stores the user's access and refresh tokens, and is created
            // automatically when the authorization flow completes for the first time.
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    user.UserId.ToString(),
                    CancellationToken.None,
                    new FileDataStore(credPath, true), redirectURI).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }
            return credential;
        }

        private Events SendRequestToGoogle(UserCredential credential)
        {
            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "My Project 88493"
            });

            // Define parameters of request.
            var request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 250;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            var events = request.Execute();
            return events;
        }
    }
}