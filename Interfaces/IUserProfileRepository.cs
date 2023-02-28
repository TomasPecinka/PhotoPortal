using PhotoPortalWebApp.Models;

namespace PhotoPortalWebApp.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<List<Photo>> GetAllUserPhotos();
        Task<List<Club>> GetAllUserClubs();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetUserByIdNoTracking(string id);
        public void Update(AppUser user);
    }
}
