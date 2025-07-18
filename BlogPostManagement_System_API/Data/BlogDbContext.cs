using BlogPostManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogPostManagement.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
    }


}
