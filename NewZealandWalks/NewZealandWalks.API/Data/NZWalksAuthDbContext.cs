using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NewZealandWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="dbContextOptions"></param>
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Role
            // C'est ici qu'on défini les Roles possibles (Reader et Writer)

            var readerRoleId = "5a16e510-a196-4f74-a08f-504042bd0164";
            var writerRoleId = "4b56fc29-4710-4f88-8af0-f0f3f387fdc4";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}