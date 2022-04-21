using MeetingDateProposer.Domain.Models.AccountModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using MeetingDateProposer.DataLayer.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MeetingDateProposer.DataLayer.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationContext _appContext;
        private readonly ILogger<DbInitializer> _logger;
        private readonly IOptions<SeededUsersOptions> _options;

        public DbInitializer(
            ApplicationContext appContext,
            ILogger<DbInitializer> logger,
            IOptions<SeededUsersOptions> options)
        {
            _appContext = appContext;
            _logger = logger;
            _options = options;
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
                Email = _options.Value.Email,
                EmailConfirmed = true,
                Id = Guid.NewGuid(),
                UserName = _options.Value.UserName,
                PasswordHash = pass.HashPassword(new AccountUser(), _options.Value.Password),
                NormalizedUserName = _options.Value.UserName.ToUpper(),
                NormalizedEmail = _options.Value.Email.ToUpper(),
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