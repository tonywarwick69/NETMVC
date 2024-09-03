using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DashboardRepository(ApplicationDBContext context, IHttpContextAccessor httpContextAccessor) { 
            _context = context;
            _httpContextAccessor = httpContextAccessor; 
        }
        public async Task<List<Club>> GetAllClubs()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userClubs = _context.Clubs.Where(r => r.AppUserId == curUser);
            return userClubs.ToList();
        }

        public async Task<List<Race>> GetAllRaces()
        {
            var curRace = _httpContextAccessor?.HttpContext?.User.GetUserId();
            var userRaces = _context.Races.Where(r => r.AppUserId == curRace);
            return userRaces.ToList();
        }
    }
}
