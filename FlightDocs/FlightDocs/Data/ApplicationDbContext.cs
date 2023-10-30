using FlightDocs.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightDocs.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        //Set up DB
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<OldVersion> OldVersions { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<PermissionDG> PermissionDG { get; set; }
        public DbSet<PermissionDTG> PermissionDTG { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
