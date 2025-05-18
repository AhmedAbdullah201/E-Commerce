namespace Persistence.Repositories
{
    public abstract class SpecificationEvaluator
    {
        public static IQueryable<T>CreateQuery<T> (IQueryable<T> InputQuery , ISpecification<T> specification) where T : class
        {
            var query = InputQuery;
            if (specification.Criteria is not null) query = query.Where(specification.Criteria);
            query = specification.IncludeExpression.Aggregate(query, (currentquery, includeexpression) 
                => currentquery.Include(includeexpression));
           
            if(specification.OrderBy is not null)
                query=query.OrderBy(specification.OrderBy);
           else if (specification.OrderByDescending is not null)
                query = query.OrderByDescending(specification.OrderByDescending);

           if (specification.IsPaginated)
                query = query.Skip(specification.Skip).Take(specification.Take);
            
            return query;
        }
    }
}
