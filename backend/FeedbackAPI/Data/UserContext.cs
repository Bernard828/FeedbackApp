using Microsoft.EntityFrameworkCore;
using FeedbackAPI.Models;

namespace FeedbackAPI.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        //public DbSet<User> Users => Set<User>();

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
