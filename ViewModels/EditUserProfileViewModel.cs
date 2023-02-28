namespace PhotoPortalWebApp.ViewModels
{
    public class EditUserProfileViewModel
    {
        public string Id { get; set; }
        public string? FullName { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
