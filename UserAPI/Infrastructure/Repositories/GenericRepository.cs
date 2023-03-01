using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Reflection;
using UserAPI.Core.Application.IRepositories;
using UserAPI.Core.Domain.Model;
using UserAPI.Infrastructure.DataContext;

namespace UserAPI.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected UserAPIDataContext _context;
        public GenericRepository(UserAPIDataContext context) => _context = context;

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> Find(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(filter);
        }

        public async Task<T> AddAsync(T entity)
        {
            UpdateTimeStamp(entity, true);
            await _context.AddAsync(entity).ConfigureAwait(false);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> AddRangeAsync(List<T> entities)
        {
            foreach (var item in entities)
            {
                UpdateTimeStamp(item, true);
            }

            await _context.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return true;
        }

        public void Update(T entity)
        {
            UpdateTimeStamp(entity, false);
            _context.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void UpdateTimeStamp(T entity, bool create)
        {
            Type type = entity.GetType();

            if (create)
            {
                type.GetProperty("AppCreationDate")?.SetValue(entity, DateTime.Now);
                if (string.IsNullOrEmpty(type.GetProperty("Uid")?.GetValue(entity, null)?.ToString()))
                    type.GetProperty("Uid")?.SetValue(entity, Guid.NewGuid().ToString());
            }
            else
            {
                type.GetProperty("AppTimeStamp")?.SetValue(entity, DateTime.Now);
            }

        }
    }
}
