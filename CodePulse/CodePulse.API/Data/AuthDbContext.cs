using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CodePulse.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
            //Batteries.Init();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Role
            // C'est ici qu'on défini les Roles possibles (Reader et Writer)
            var readerRoleId = "28d65a5b-a7db-4850-b380-83591f7d7531";
            var writerRoleId = "9740f16c-24a1-4224-a7be-1bb00b7c6892";

            // Create Reader and Writer Role
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "READER",
                },
                new IdentityRole()
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "WRITER"
                }
            };

            // Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            // Identifiants fixes
            var adminUserId = "edc267ec-d43c-4e3b-8108-a1a1f819906d";

            // Crée l'utilisateur admin
            var admin = new IdentityUser
            {
                Id = adminUserId,
                UserName = "admin@codepulse.com",
                Email = "admin@codepulse.com",
                NormalizedEmail = "ADMIN@CODEPULSE.COM",
                NormalizedUserName = "ADMIN@CODEPULSE.COM",
                EmailConfirmed = true,
                SecurityStamp = "e5b6dc2f-2949-4f33-8a89-140d01b53df1", // Valeur fixe
                ConcurrencyStamp = "fc3e0935-ef8f-42b6-b6ff-1880e3d4a121", // Valeur fixe
                PasswordHash = "AQAAAAIAAYagAAAAEKG1GahV09HZ9Tk/MKKWng+/Q1fP1X0p8VW3rtrNYn/whyDjLSUBnJXbBBsLj63bZQ=="
            };


            // Ajoute l’utilisateur
            builder.Entity<IdentityUser>().HasData(admin);

            // Affecte les rôles admin
            var adminRoles = new List<IdentityUserRole<string>>
            {
                new() { UserId = adminUserId, RoleId = readerRoleId },
                new() { UserId = adminUserId, RoleId = writerRoleId }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
