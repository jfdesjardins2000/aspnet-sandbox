using CodePulse.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace CodePulse.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Batteries.Init();
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
    }
}
