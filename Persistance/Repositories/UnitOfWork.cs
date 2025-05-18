using Persistence.Identity;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _context)
        : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = new Dictionary<string, object>();
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if (_repositories.ContainsKey(typeName))
                return (IGenericRepository<TEntity, TKey>)_repositories[typeName];

            var repo = new GenericRepository<TEntity, TKey>(_context);
            _repositories[typeName] = repo;

            return repo;
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
