using Google.Apis.Calendar.v3.Data;
using MeetingDateProposer.BusinessLayer;
using MeetingDateProposer.BusinessLayer.DbInteractionServices;
using MeetingDateProposer.BusinessLayer.Formatters;
using MeetingDateProposer.BusinessLayer.Providers;
using MeetingDateProposer.DataLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace MeetingDateProposer.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMeetingService, MeetingService>();
            services.AddScoped<ICalendarProvider, GoogleCalendarProvider>();
            services.AddScoped<ICalendarEventFormatter<IList<Event>>, GoogleCalendarEventFormatter>();
            services.AddScoped<ICalendarCalculator, CalendarCalculator>();
            services.AddScoped<IDbInitializer, DbInitializer>();
        }
    }
}
