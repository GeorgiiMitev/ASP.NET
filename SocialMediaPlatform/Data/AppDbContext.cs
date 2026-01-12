
using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Models;
using System.Reflection.Metadata;

namespace SocialMediaPlatform.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }
       

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One-to-one relationship

            modelBuilder.Entity<User>()
                .HasOne(u => u.Blog)
                .WithOne(b => b.User)
                .HasForeignKey<Blog>(b => b.UserId);

            modelBuilder.Entity<Blog>()
                .HasMany(b => b.Posts)
                .WithOne(p => p.Blog)
                .HasForeignKey(p => p.BlogId);

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Tags)
                .WithMany(p => p.Posts);
                
        }
        public DbSet<SocialMediaPlatform.Models.Tag> Tag { get; set; } = default!;

        
    }
}
