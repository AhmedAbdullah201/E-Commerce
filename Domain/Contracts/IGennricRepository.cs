using Domain.Models;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification);
        Task<TEntity?> GetAsync(ISpecification<TEntity> specification);
        Task<int> GetCountAsync(ISpecification<TEntity> specification);
        Task<TEntity?> GetAsync(TKey key);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
