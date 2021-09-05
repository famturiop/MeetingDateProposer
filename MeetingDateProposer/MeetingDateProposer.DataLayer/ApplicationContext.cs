using System;
using System.Collections.Generic;
using MeetingDateProposer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace MeetingDateProposer.DataLayer
{
    public sealed class ApplicationContext : IdentityDbContext<User,IdentityRole<Guid>,Guid>
    {
        private readonly IConfiguration _config;
        public ApplicationContext(DbContextOptions options) : base(options)
        {
            
        }
        
        public override DbSet<User> Users { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public override DbSet<IdentityRole<Guid>> Roles { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(k => k.Id);
            modelBuilder.Entity<Calendar>().HasKey(k => k.Id);
            modelBuilder.Entity<CalendarEvent>().HasKey(k => k.Id);
            modelBuilder.Entity<Meeting>().HasKey(k => k.Id);
            
            modelBuilder.Entity<User>().Ignore(c => c.Credentials);
            //modelBuilder.Entity<User>()
            //    .Property(c => c.Calendars)
            //    .IsRequired(false);
            //modelBuilder.Entity<Calendar>()
            //    .Property(c => c.UserCalendar)
            //    .IsRequired(false);
            modelBuilder.Entity<CalendarEvent>()
                .Property(c => c.EventStart)
                .IsRequired(true);
            modelBuilder.Entity<CalendarEvent>()
                .Property(c => c.EventEnd)
                .IsRequired(true);

            modelBuilder.Entity<User>()
                .Property(c => c.Id)
                .HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Meeting>()
                .Property(c => c.Id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<User>()
                .HasMany<Calendar>(c => c.Calendars)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(true);
            modelBuilder.Entity<Calendar>()
                .HasMany<CalendarEvent>(c => c.UserCalendar)
                .WithOne(c => c.Calendar)
                .HasForeignKey(c => c.CalendarId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(true);
            modelBuilder.Entity<Meeting>()
                .HasMany<User>(c => c.ConnectedUsers)
                .WithMany(c => c.UserMeetings);

            SeedRolesAndUsers(modelBuilder);
            
            base.OnModelCreating(modelBuilder);
        }

        private void SeedRolesAndUsers(ModelBuilder modelBuilder)
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
            PasswordHasher<User> pass = new PasswordHasher<User>();
            User admin = new User
            {
                Email = "test@test.com",
                EmailConfirmed = true,
                Id = Guid.Parse("4f4d9c6c-e823-457e-9bfa-b2d15922ca17"),
                Name = "admin",
                UserName = "test@test.com",
                PasswordHash = pass.HashPassword(new User(), "qwerty"),
                NormalizedUserName = "TEST@TEST.COM",
                NormalizedEmail = "TEST@TEST.COM",
                LockoutEnabled = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };


            var identityUserRole = new IdentityUserRole<Guid>
            {
                UserId = admin.Id,
                RoleId = adminRole.Id
            };
            modelBuilder.Entity<IdentityRole<Guid>>().HasData(new IdentityRole<Guid>[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { admin });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>[] {identityUserRole});
        }
    }
}
