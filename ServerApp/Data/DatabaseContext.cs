using Microsoft.EntityFrameworkCore;
using ServerApp.Data.Models;

namespace ServerApp.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the one-to-many relationship between User and UserGroup
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserGroup)
                .WithMany(g => g.Users)
                .HasForeignKey(u => u.UserGroupId);
        }
    }
}
