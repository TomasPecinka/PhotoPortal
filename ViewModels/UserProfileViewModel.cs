using PhotoPortalWebApp.Models;

namespace PhotoPortalWebApp.ViewModels
{
    public class UserProfileViewModel
    {
        public List<Photo>? Photos { get; set; }
        public List<Club>? Clubs { get; set; }
    }
}
