using RunGroupWebApp.Models;

namespace RunGroupWebApp.Interfaces
{
    public interface IClubRepository : IBaseRepository<Club>
    {
        Task<IEnumerable<Club>> GetClubByCity(string city);
    }
}
