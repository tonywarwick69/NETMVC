using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Repository
{
    public class ClubRepository : IClubRepository
    {
        public readonly ApplicationDBContext _context;
        public ClubRepository(ApplicationDBContext context) {
            _context = context;
        }

        public bool Add(Club entity)
        {
            _context.Add(entity);
            return Save();
        }

        public bool Delete(Club club)
        {
            _context.Remove(club);
            return Save();
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _context.Clubs.ToListAsync();
            
        }

        public async Task<Club> GetByIdAsync(int id)
        {
            return await _context.Clubs.Include(a => a.Address).FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Club> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Clubs.Include(a => a.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            return await _context.Clubs.Include(a => a.Address).Where(i => i.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges(); 
            return saved > 0 ? true : false;
        }

        public bool Update(Club entity)
        {
            _context.Update(entity);
            return Save();
        }
    }
}
