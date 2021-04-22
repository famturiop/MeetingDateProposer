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

            var credential = GetAccessToGoogle(Scopes);
            var events = SendRequestToGoogle(credential);


            var calendar = new Calendar();
            calendar.UserCalendar = new List<CalendarEvent>();
            //calendar.UserCalendar.Add(calendarEvent);

            foreach (var eventItem in events.Items)
            {
                var calendarEvent = new CalendarEvent(); // что происходит с созданием обьекта в цикле? ссылка на предыдущий теряется на каждой итерации?
                calendarEvent.EventStart = eventItem.Start.DateTime;
                calendarEvent.EventEnd = eventItem.End.DateTime;

                calendar.UserCalendar.Add(calendarEvent); // передал ссылку на обьект, а не сам обьект?
            }
        }
        // можно разбить на часть приватных методов, 10 строк?
        // выдает объект календарь

        private UserCredential GetAccessToGoogle(string[] Scopes)
        {
            UserCredential credential;
            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                var credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
                return credential;
            }
        }

        private Events SendRequestToGoogle(UserCredential credential)
        {
            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "Temp"
            });

            // Define parameters of request.
            var request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            var events = request.Execute();
            return events;
        }
    }
}