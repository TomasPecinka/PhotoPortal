using PhotoPortalWebApp.Models;

namespace PhotoPortalWebApp.Interfaces
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetAllAsync();
        Task<Photo> GetByIdAsync(int id);
        Task<Photo> GetByIdAsyncNoTracking(int id);
        Task AddAsync(Photo photo);
        void Update(Photo photo);
        void Delete(Photo photo);
    }
}
