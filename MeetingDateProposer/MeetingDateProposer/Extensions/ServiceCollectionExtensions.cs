using Google.Apis.Calendar.v3.Data;
using MeetingDateProposer.BusinessLayer;
using MeetingDateProposer.BusinessLayer.DbInteractionServices;
using MeetingDateProposer.BusinessLayer.Formatters;
using MeetingDateProposer.BusinessLayer.Providers;
using MeetingDateProposer.DataLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using MeetingDateProposer.BusinessLayer.Options;
using MeetingDateProposer.DataLayer.Options;
using Microsoft.Extensions.Configuration;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using MeetingDateProposer.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace MeetingDateProposer.Extensions
{
    public static class ServiceCollectionExtensions
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

        public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SeededUsersOptions>(options =>
                configuration.GetSection(SeededUsersOptions.Admin).Bind(options));
            services.Configure<CalendarOptions>(options =>
                configuration.GetSection(CalendarOptions.GoogleCalendar).Bind(options));
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
