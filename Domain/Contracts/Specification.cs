using System.Linq.Expressions;

namespace Domain.Contracts
{
    public interface ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>>? Criteria { get; }
        public List<Expression<Func<T, object>>> IncludeExpression { get; } 
        public Expression<Func<T, object>> OrderBy { get;}
        public Expression<Func <T, object>> OrderByDescending { get; }
        public int Take {  get; }
        public int Skip { get;  }
        public bool IsPaginated { get; }

    }
}
