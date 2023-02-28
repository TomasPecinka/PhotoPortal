using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace PhotoPortalWebApp.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
