using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PhotoPortalWebApp.Data;
using PhotoPortalWebApp.Interfaces;
using PhotoPortalWebApp.Models;

namespace PhotoPortalWebApp.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly PhotoPortalDbContext _context;

        public PhotoRepository(PhotoPortalDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Photo photo)
        {
            await _context.Photos.AddAsync(photo);
            await _context.SaveChangesAsync();
        }

        public void Delete(Photo photo)
        {
            _context.Photos.Remove(photo);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Photo>> GetAllAsync()
        {
            return await _context.Photos.ToListAsync();
        }

        public async Task<Photo> GetByIdAsync(int id)
        {
            return await _context.Photos.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Photo> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Photos.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public void Update(Photo club)
        {
            _context.Photos.Update(club);
            _context.SaveChanges();
        }
    }
}
