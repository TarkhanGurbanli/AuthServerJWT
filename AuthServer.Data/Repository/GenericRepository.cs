using AuthServer.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthServer.Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        // GenericRepository'nin kurucu metodunu güncelledik
        public GenericRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            //Eğer entity null deyilsə, yani bir obyekt varsa, bu obyektin vəziyyətini EntityState.
            //Detached olaraq təyin et. Bu əməliyyat əsasən obyektin bağlamdan ayrılması
            //və izlənməməsi üçün həyata keçirilir.
            if (entity != null)
                _context.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
    }
}
