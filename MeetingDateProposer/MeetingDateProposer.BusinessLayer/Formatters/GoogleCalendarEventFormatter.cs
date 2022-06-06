using Google.Apis.Calendar.v3.Data;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MeetingDateProposer.BusinessLayer.Formatters
{
    public class GoogleCalendarEventFormatter : ICalendarEventFormatter<IList<Event>>
    {
        private readonly ILogger<GoogleCalendarEventFormatter> _logger;
        public GoogleCalendarEventFormatter(ILogger<GoogleCalendarEventFormatter> logger)
        {
            _logger = logger;
        }
        public List<CalendarEvent> FormatCalendarEvents(IList<Event> events)
        {
            var calendarEvents = new List<CalendarEvent>();

            foreach (var eventItem in events)
            {
                try
                {
                    if (DoesBlockCalendar(eventItem))
                    {
                        calendarEvents.Add(new CalendarEvent
                        {
                            EventStart = IsFullDayEvent(eventItem)
                                ? ConvertStartDate(eventItem).ToUniversalTime()
                                : ((DateTime)eventItem.Start.DateTime).ToUniversalTime(),
                            EventEnd = IsFullDayEvent(eventItem)
                                ? ConvertEndDate(eventItem).ToUniversalTime()
                                : ((DateTime)eventItem.End.DateTime).ToUniversalTime()
                        });
                    }
                }
                catch (FormatException e)
                {
                    _logger.LogWarning(e, "Incorrect string parse format in {eventItem}", eventItem);
                }
                catch (Exception e)
                {
                    if (e is InvalidOperationException || e is ArgumentNullException)
                        _logger.LogWarning(e, "Unknown event containing null was detected" +
                                              " and skipped in {eventItem}", eventItem);
                }
            }

            return calendarEvents;
        }

        private static DateTime ConvertStartDate(Event calEvent)
        {
            return DateTime.Parse(calEvent.Start.Date);
        }

        private static DateTime ConvertEndDate(Event calEvent)
        {
            return DateTime.Parse(calEvent.End.Date);
        }

        private static bool DoesBlockCalendar(Event calEvent)
        {
            return calEvent.Transparency == null || calEvent.Transparency == "opaque";
        }

        private static bool IsFullDayEvent(Event calEvent)
        {
            return calEvent.Start.DateTime == null &&
                   calEvent.End.DateTime == null &&
                   calEvent.Start.Date != null &&
                    calEvent.End.Date != null;
        }
    }
}
