using PhotoPortalWebApp.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace PhotoPortalWebApp.ViewModels
{
    public class CreatePhotoViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UploadDateTime { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public IFormFile Image { get; set; }
        public PhotoCategory PhotoCategory { get; set; }
        public string AppUserId { get; set; }
    }
}
