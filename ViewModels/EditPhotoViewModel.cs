using PhotoPortalWebApp.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhotoPortalWebApp.ViewModels
{
    public class EditPhotoViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UploadDateTime { get; set; }
        public PhotoCategory PhotoCategory { get; set; }
        public IFormFile Image { get; set; }
        public string? ImageUrl { get; set; }
        public string AppUserId { get; set; }
    }
}
