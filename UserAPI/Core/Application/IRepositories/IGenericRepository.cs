using System.Linq.Expressions;

namespace UserAPI.Core.Application.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(string id);
        Task<T?> Find(Expression<Func<T, bool>> filter);
        Task<T> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entities);
        void Update(T entity);
        void Delete(T entity);
    }
}