using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MeetingDateProposer.DataLayer;
using MeetingDateProposer.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using MeetingDateProposer.Domain.Models.AccountModels;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using Microsoft.Extensions.Configuration;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var tmp = new Test();
            tmp.TestThings();
        }

    }

    public class Test
    {
        private IConfiguration _config;
        public void TestThings()
        {
            DbContextOptionsBuilder tmp = new DbContextOptionsBuilder();
            tmp.UseSqlServer("Server=ADMIN-PC;Database=MeetingDB;Trusted_Connection=True;");
            ApplicationContext appContext = new ApplicationContext(tmp.Options);
            //User b = RemoveUserFromDb(Guid.Parse("C7CCA55A-B425-4D0C-A818-810CDAB071F6"), appContext);
            SeedRolesAndUsers();
        }

        public AccountUser RemoveUserFromDb(Guid id, ApplicationContext appContext)
        {
            //if (appContext.Users.Any(u => u.Id == id))
            //{
            var user = appContext.Users.First(u => u.Id == id);
            appContext.Users.Remove(user);
            appContext.SaveChanges();
            return user;
            //}
            //else
            //{
            //    return null;
            //}
        }

        public void SeedRolesAndUsers()
        {

            IdentityRole<Guid> adminRole = new IdentityRole<Guid>
            {
                Id = Guid.Parse("D8DCD30C-1005-405A-D397-08D96A226C76"),
                Name = "admin",
                NormalizedName = "ADMIN"
            };
            IdentityRole<Guid> userRole = new IdentityRole<Guid>
            {
                Id = Guid.Parse("5d106043-53f8-4a1b-8459-e5409d1b2b0a"),
                Name = "user",
                NormalizedName = "USER"
            };
            //PasswordHasher<User> pass = new PasswordHasher<User>();
            //User admin = new User
            //{
            //    Email = "test@test.com",
            //    EmailConfirmed = true,
            //    Id = Guid.Parse("4f4d9c6c-e823-457e-9bfa-b2d15922ca17"),
            //    Name = "admin",
            //    UserName = "test@test.com",
            //    PasswordHash = pass.HashPassword(new User(), "qwerty"),
            //    NormalizedUserName = "TEST@TEST.COM",
            //    NormalizedEmail = "TEST@TEST.COM",
            //    LockoutEnabled = true,
            //    SecurityStamp = Guid.NewGuid().ToString()
            //};
            var builder = new ConfigurationBuilder().AddUserSecrets<Program>();
            _config = builder.Build();
            ApplicationUser admin = _config.GetSection("admin").Get<ApplicationUser>();
            var identityUserRole = new IdentityUserRole<Guid>
            {
                UserId = admin.Id,
                RoleId = adminRole.Id
            };
            
        }
    }
}

