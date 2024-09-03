using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly ApplicationDBContext _dbContext;
        public UserRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext; 
        }
        public bool Delete(AppUser user)
        {
            _dbContext.Remove(user);
            if (user == null) return true;
            else return false;

        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<AppUser> GetUserById(string userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public bool Save()
        {
            var savedUser = _dbContext.SaveChanges();
            return savedUser > 0 ? true : false;
        }

        public bool Update(AppUser user)
        {
            _dbContext.Update(user);
            return Save();
        }
    }
}
