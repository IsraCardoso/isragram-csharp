using isragram_csharp.Models;
using Microsoft.EntityFrameworkCore;

namespace DevagramCSharp.Models
{
    public class IsragramContext : DbContext
    {
        public IsragramContext(DbContextOptions<IsragramContext> option) : base(option)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
