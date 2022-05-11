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
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                services.Configure<SeededUsersOptions>(options =>
                    configuration.GetSection(SeededUsersOptions.Admin).Bind(options));
                services.Configure<ApiKeysOptions>(options =>
                    configuration.GetSection(ApiKeysOptions.GoogleCalendarApiKeys).Bind(options));
                services.Configure<CalendarOptions>(options =>
                    configuration.GetSection(CalendarOptions.GoogleCalendar).Bind(options));
            }
            else
            {
                var client = GetAzureKeyVaultClient();
                ConfigureOptionsWithAzureKeyVaultSecrets(services, configuration, client);
            }
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            }
            else
            {
                var client = GetAzureKeyVaultClient();
                services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(client.GetSecret("ConnectionStrings-DefaultConnection").Value.Value));
            }
        }

        private static SecretClient GetAzureKeyVaultClient()
        {
            string keyVaultName = Environment.GetEnvironmentVariable("KEY_VAULT_NAME");
            var kvUri = "https://" + keyVaultName + ".vault.azure.net";

            return new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
        }

        private static void ConfigureOptionsWithAzureKeyVaultSecrets(
            IServiceCollection services,
            IConfiguration configuration,
            SecretClient client)
        {
            services.Configure<SeededUsersOptions>(options =>
            {
                options.UserName = client.GetSecret("SeededUsers-Admin-UserName").Value.Value;
                options.Password = client.GetSecret("SeededUsers-Admin-Password").Value.Value;
                options.Email = client.GetSecret("SeededUsers-Admin-Email").Value.Value;
            });

            services.Configure<ApiKeysOptions>(options =>
            {
                options.ClientId = client.GetSecret("ApiKeys-GoogleCalendar-ClientId").Value.Value;
                options.ClientSecret = client.GetSecret("ApiKeys-GoogleCalendar-ClientSecret").Value.Value;
            });

            services.Configure<CalendarOptions>(options =>
                    configuration.GetSection(CalendarOptions.GoogleCalendar).Bind(options));
        }
    }
}
