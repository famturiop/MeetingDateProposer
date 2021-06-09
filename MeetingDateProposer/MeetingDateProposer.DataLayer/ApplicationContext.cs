using MeetingDateProposer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MeetingDateProposer.DataLayer
{
    public sealed class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options): base(options)
        {
            
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<Meeting> Meetings { get; set; }


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

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
            //var data = (JObject)JsonConvert.DeserializeObject("appsettings.json");
            //string basepath = data["B"].Value<string>();
            

            //IConfigurationRoot Configuration = new ConfigurationBuilder().SetBasePath(basepath).AddJsonFile("appsettings.json").Build();

            //optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        //}
    }
}
