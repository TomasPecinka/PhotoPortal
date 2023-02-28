using PhotoPortalWebApp.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoPortalWebApp.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UploadDateTime { get; set; }
        public PhotoCategory PhotoCategory { get; set; }
        public string ImageUrl { get; set; }
        
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}