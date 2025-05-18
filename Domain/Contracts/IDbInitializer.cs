namespace Domain.Contracts
{
    public interface IDbInitializer
    {
        Task InitializeAsync();
        Task InitializeIdentityAsync();
    }
}
