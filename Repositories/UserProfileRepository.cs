using Microsoft.EntityFrameworkCore;
using PhotoPortalWebApp.Data;
using PhotoPortalWebApp.Interfaces;
using PhotoPortalWebApp.Models;

namespace PhotoPortalWebApp.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly PhotoPortalDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserProfileRepository(PhotoPortalDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Club>> GetAllUserClubs()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userClubs = _context.Clubs.Where(r => r.AppUser.Id == currentUser);
            return await userClubs.ToListAsync();
        }

        public async Task<List<Photo>> GetAllUserPhotos()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userPhotos = _context.Photos.Where(r => r.AppUser.Id == currentUser);
            return await userPhotos.ToListAsync();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByIdNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public void Update(AppUser user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
