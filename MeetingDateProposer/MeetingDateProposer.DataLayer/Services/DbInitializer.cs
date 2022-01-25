using MeetingDateProposer.Domain.Models.AccountModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace MeetingDateProposer.DataLayer.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationContext _appContext;
        private readonly IConfiguration _configuration;

        public DbInitializer(ApplicationContext appContext, IConfiguration configuration)
        {
            _appContext = appContext;
            _configuration = configuration;
        }

        public void Initialize()
        {
            _appContext.Database.Migrate();
        }

        public void Seed()
        {
            IdentityRole<Guid> adminRole = new IdentityRole<Guid>
            {
                Id = Guid.NewGuid(),
                Name = "admin",
                NormalizedName = "ADMIN"
            };
            IdentityRole<Guid> userRole = new IdentityRole<Guid>
            {
                Id = Guid.NewGuid(),
                Name = "user",
                NormalizedName = "USER"
            };

            PasswordHasher<AccountUser> pass = new PasswordHasher<AccountUser>();
            AccountUser admin = new AccountUser
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
            }
            if (!_appContext.Users.Any())
            {
                _appContext.Users.Add(admin);
                _appContext.UserRoles.Add(identityAdminRole);
            }

            _appContext.SaveChanges();
        }
    }
}