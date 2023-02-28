using PhotoPortalWebApp.Models;

namespace PhotoPortalWebApp.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetByIdAsync(string id);
    }
}
