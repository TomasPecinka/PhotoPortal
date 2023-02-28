using PhotoPortalWebApp.Data.Enum;
using PhotoPortalWebApp.Models;

namespace PhotoPortalWebApp.ViewModels
{
    public class EditClubViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public string? URL { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public ClubCategory ClubCategory { get; set; }
    }
}
