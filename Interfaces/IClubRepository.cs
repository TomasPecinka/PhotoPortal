using Newtonsoft.Json.Bson;
using PhotoPortalWebApp.Models;

namespace PhotoPortalWebApp.Interfaces
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetAll();
        Task<Club> GetByIdAsync(int id);
        Task<Club> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Club>> GetClubByCity(string city);
        void Add(Club club);
        void Update(Club club);
        void Delete(Club club);
    }
}
