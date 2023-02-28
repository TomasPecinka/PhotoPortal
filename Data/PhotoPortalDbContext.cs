using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoPortalWebApp.Models;

namespace PhotoPortalWebApp.Data
{
    public class PhotoPortalDbContext : IdentityDbContext<AppUser>
    {
        public PhotoPortalDbContext(DbContextOptions<PhotoPortalDbContext> options) : base(options)
        {
        }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
