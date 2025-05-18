    namespace Shared
{
    public record PaginatedResult<TData>(int PageIndex, int PageSize, int Count , IEnumerable<TData> Data);   
}
