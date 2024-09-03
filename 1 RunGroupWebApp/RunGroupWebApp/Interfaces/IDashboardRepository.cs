using RunGroupWebApp.Models;

namespace RunGroupWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Race>> GetAllRaces();
        Task<List<Club>> GetAllClubs();
    }
}
