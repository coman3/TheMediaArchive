using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Coman3.Models.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Serie> Series { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public ApplicationDbContext() : base("DefaultConnection", false)
        {
            
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
    }
}