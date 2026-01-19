
using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure One-to-One
            modelBuilder.Entity<User>()
                .HasOne(u => u.Blog)
                .WithOne(b => b.User)
                .HasForeignKey<Blog>(b => b.UserId);

            // Configure One-to-Many
            modelBuilder.Entity<Blog>()
                .HasMany(b => b.Posts)
                .WithOne(p => p.Blog)
                .HasForeignKey(p => p.BlogId);

            // Configuration Many-to-Many:
            // a join table (PostTag) is automatically created by EF
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Tags)
                .WithMany(t => t.Posts);

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)      // A Post has many Comments
                .WithOne(c => c.Post)          // Each Comment has one Post
                .HasForeignKey(c => c.PostId)  // The link is the PostId column
                .OnDelete(DeleteBehavior.Cascade); // Deleting a Post deletes its Comments

            //Same as Above example
            //modelBuilder.Entity<Comment>()
            //    .HasOne(c => c.Post)      // A Post has many Comments
            //    .WithMany(p => p.Comments)          // Each Comment has one Post
            //    .HasForeignKey(c => c.PostId)  // The link is the PostId column
            //    .OnDelete(DeleteBehavior.Cascade); // Deleting a Post deletes its Comments

            //Important: There is no functional difference between the above two configurations.
            //Both snippets describe the exact same relationship between Post and Comment.
        }

    }
}
