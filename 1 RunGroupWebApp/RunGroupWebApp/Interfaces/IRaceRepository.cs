using RunGroupWebApp.Models;

namespace RunGroupWebApp.Interfaces
{
    public interface IRaceRepository : IBaseRepository<Race>
    {
        public Task<IEnumerable<Race>> GetAllRacesByCity(string city);
        public Task<Race> GetByIdAsyncNoTracking(int id);

    }
}
