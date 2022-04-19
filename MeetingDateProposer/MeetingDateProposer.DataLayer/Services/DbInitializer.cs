using MeetingDateProposer.Domain.Models.AccountModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace MeetingDateProposer.DataLayer.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationContext _appContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(
            ApplicationContext appContext, 
            IConfiguration configuration,
            ILogger<DbInitializer> logger)
        {
            _appContext = appContext;
            _configuration = configuration;
            _logger = logger;
        }

        public void Initialize()
        {
            var migrations = _appContext.Database.GetPendingMigrations().ToList();
            if (migrations.Count > 0)
            {
                _appContext.Database.Migrate();
                _logger.LogInformation("Applied {1} pending migrations to the database.", migrations.Count);
            }
        }

        public void Seed()
        {
            var adminRole = new IdentityRole<Guid>
            {
                Id = Guid.NewGuid(),
                Name = "admin",
                NormalizedName = "ADMIN"
            };
            var userRole = new IdentityRole<Guid>
            {
                Id = Guid.NewGuid(),
                Name = "user",
                NormalizedName = "USER"
            };

            var pass = new PasswordHasher<AccountUser>();
            var admin = new AccountUser
            {
                Email = _configuration["admin:Email"],
                EmailConfirmed = true,
                Id = Guid.NewGuid(),
                UserName = _configuration["admin:UserName"],
                PasswordHash = pass.HashPassword(new AccountUser(), _configuration["admin:Password"]),
                NormalizedUserName = _configuration["admin:NormalizedUserName"],
                NormalizedEmail = _configuration["admin:NormalizedEmail"],
                LockoutEnabled = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var identityAdminRole = new IdentityUserRole<Guid>
            {
                UserId = admin.Id,
                RoleId = adminRole.Id
            };

            if (!_appContext.Roles.Any())
            {
                _appContext.Roles.Add(adminRole);
                _appContext.Roles.Add(userRole);
                _logger.LogInformation("Added admin and user roles entries to the database.");
            }
            if (!_appContext.Users.Any())
            {
                _appContext.Users.Add(admin);
                _appContext.UserRoles.Add(identityAdminRole);
                _logger.LogInformation("Added admin as a user entry to the database.");
            }

            _appContext.SaveChanges();
        }
    }
}