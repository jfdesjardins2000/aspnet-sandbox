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
    }
}
