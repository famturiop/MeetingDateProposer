using MeetingDateProposer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingDateProposer.DataLayer
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<Goose> Gooses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-DBU0IR9;Database=testdb;Trusted_Connection=True;");
        }
    }
}
