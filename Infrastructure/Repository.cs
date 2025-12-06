
using DemoDangTin.EF;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DemoDangTin.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _entities;
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _entities.FindAsync(id);
            if (entity == null) return null!;
            return entity;
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public Task SaveChangeAsync() => _dbContext.SaveChangesAsync();
    }
}
