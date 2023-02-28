using Microsoft.EntityFrameworkCore;
using PhotoPortalWebApp.Data;
using PhotoPortalWebApp.Interfaces;
using PhotoPortalWebApp.Models;

namespace PhotoPortalWebApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PhotoPortalDbContext _context;

        public UserRepository(PhotoPortalDbContext context)
        {
            _context = context;
        }

        public async Task<AppUser> GetByIdAsync(string id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
