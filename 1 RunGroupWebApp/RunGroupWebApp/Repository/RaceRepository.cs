using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Repository
{
    public class RaceRepository : IRaceRepository
    {
        public readonly ApplicationDBContext _context;
        public RaceRepository(ApplicationDBContext context) {  
            _context = context; }

        public bool Add(Race entity)
        {
            _context.Add(entity);
            return Save();
        }

        public bool Delete(Race race)
        {
            _context.Remove(race);
            return Save();  
        }

        public async Task<IEnumerable<Race>> GetAll()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<IEnumerable<Race>> GetAllRacesByCity(string city)
        {
            return await _context.Races.Include(a => a.Address).Where(r => r.Address.City.Contains(city)).ToListAsync();    
        }

        public async Task<Race> GetByIdAsync(int id)
        {
            return await _context.Races.Include(a => a.Address).FirstOrDefaultAsync(r => r.Id == id);   
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >0 ? true : false;
        }

        public bool Update(Race entity)
        {
            _context.Update(entity);
            return Save();
        }
    }
}
