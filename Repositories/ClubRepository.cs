using Microsoft.EntityFrameworkCore;
using PhotoPortalWebApp.Data;
using PhotoPortalWebApp.Interfaces;
using PhotoPortalWebApp.Models;

namespace PhotoPortalWebApp.Repositories
{
    public class ClubRepository : IClubRepository
    {
        private readonly PhotoPortalDbContext _context;

        public ClubRepository(PhotoPortalDbContext context)
        {
            _context = context;
        }

        public void Add(Club club)
        {
            _context.Clubs.Add(club);
            _context.SaveChanges();
        }

        public void Delete(Club club)
        {
            _context.Clubs.Remove(club);
            _context.SaveChanges();
        }

        public async Task<Club> GetByIdAsync(int id)
        {
            return await _context.Clubs.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Club> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Clubs.Include(x => x.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            return await _context.Clubs.Where(x => x.Address.City.Contains(city)).ToListAsync();
        }
        public void Update(Club club)
        {
            _context.Clubs.Update(club);
            _context.SaveChanges();
        }
        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _context.Clubs.ToListAsync();
        }
    }
}
