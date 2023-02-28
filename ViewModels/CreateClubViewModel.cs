using PhotoPortalWebApp.Data.Enum;
using PhotoPortalWebApp.Models;

namespace PhotoPortalWebApp.ViewModels
{
    public class CreateClubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public ClubCategory ClubCategory { get; set; }
        public Address? Address { get; set; }
        public string? AppUserId { get; set; }
    }
}
