using Bloggr.API.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggr.API.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed Roles into Database
            var userRoleId = "d4162388-82fb-43e8-a918-270d38762156";
            var adminRoleId = "a699b2de-7d03-4515-913f-6a722e3f272e";

            var roles = new List<IdentityRole>{
                new(){
                    Id=userRoleId,
                    ConcurrencyStamp=userRoleId,
                    Name="User",
                    NormalizedName="USER"
                },
                new(){
                    Id=adminRoleId,
                    ConcurrencyStamp=adminRoleId,
                    Name="Admin",
                    NormalizedName="ADMIN"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}