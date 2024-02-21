using Microsoft.EntityFrameworkCore;
using TutorialWebApp.Models.Domain;

namespace TutorialWebApp.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<TutorialPost> TutorialPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TutorialPostLike> TutorialPostLike { get; set; }
        public DbSet<TutorialComment> TutorialComment { get; set; }
    }
}
