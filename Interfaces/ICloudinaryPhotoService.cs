using CloudinaryDotNet.Actions;

namespace PhotoPortalWebApp.Interfaces
{
    public interface ICloudinaryPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
