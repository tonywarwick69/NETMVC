using RunGroupWebApp.Models;

namespace RunGroupWebApp.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> GetUserById(string userId);
        bool Update(AppUser user);
        bool Delete(AppUser user);
        bool Save();

    }
}
