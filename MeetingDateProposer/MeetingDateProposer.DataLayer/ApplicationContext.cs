using MeetingDateProposer.Domain.Models.AccountModels;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace MeetingDateProposer.DataLayer
{
    public sealed class ApplicationContext : IdentityDbContext<AccountUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public override DbSet<AccountUser> Users { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Calendar> Calendars { get; set; }

        public DbSet<CalendarEvent> CalendarEvents { get; set; }

        public DbSet<Meeting> Meetings { get; set; }

        public override DbSet<IdentityRole<Guid>> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.HasKey(k => k.Id);
                b.Ignore(c => c.Credentials);
                b.Property(c => c.Id).HasDefaultValueSql("NEWID()");
                b.HasMany<Calendar>(c => c.Calendars)
                    .WithOne(c => c.User)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired(true);

            });

            modelBuilder.Entity<Calendar>(b =>
            {
                b.HasKey(k => k.Id);
                b.HasMany<CalendarEvent>(c => c.UserCalendar)
                    .WithOne(c => c.Calendar)
                    .HasForeignKey(c => c.CalendarId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired(true);
            });

            modelBuilder.Entity<CalendarEvent>(b =>
            {
                b.HasKey(k => k.Id);
                b.Property(c => c.EventStart)
                    .IsRequired(true);
                b.Property(c => c.EventEnd)
                    .IsRequired(true);
            });

            modelBuilder.Entity<Meeting>(b =>
            {
                b.HasKey(k => k.Id);
                b.Property(c => c.Id)
                    .HasDefaultValueSql("NEWID()");
                b.HasMany<ApplicationUser>(c => c.ConnectedUsers)
                    .WithMany(c => c.UserMeetings);
            });

            modelBuilder.Entity<AccountUser>(b =>
            {
                b.HasOne<ApplicationUser>()
                    .WithOne(c => c.AccountUser)
                    .HasForeignKey<ApplicationUser>(c => c.AccountUserId)
                    .IsRequired(false);
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
