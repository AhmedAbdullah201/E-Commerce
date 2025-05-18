using Persistence.Identity;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext _context) :
        IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {

        public void Add(TEntity entity) => _context.Set<TEntity>().Add(entity);

        public void Delete(TEntity entity) => _context.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges)
            => trackChanges ? await _context.Set<TEntity>().ToListAsync()
            : await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetAsync(TKey key) => await _context.Set<TEntity>().FindAsync(key);

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification) 
            => await SpecificationEvaluator.CreateQuery(_context.Set<TEntity>(), specification).ToListAsync();

        public async Task<TEntity?> GetAsync(ISpecification<TEntity> specification) 
            => await SpecificationEvaluator.CreateQuery(_context.Set<TEntity>(), specification).SingleOrDefaultAsync();

        public async Task<int> GetCountAsync(ISpecification<TEntity> specification)
            => await SpecificationEvaluator.CreateQuery(_context.Set<TEntity>(), specification).CountAsync();
    }
}
