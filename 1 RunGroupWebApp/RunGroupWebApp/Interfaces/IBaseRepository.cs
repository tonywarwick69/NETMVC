using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByIdAsync(int id);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool Save();

    }
}
