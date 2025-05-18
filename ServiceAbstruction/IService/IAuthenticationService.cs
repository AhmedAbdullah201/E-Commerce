using Shared.Dtos.AuthenticationDto;


namespace ServiceAbstraction.IService
{
    public interface IAuthenticationService
    {
        Task<UserResponse> LoginAsync(LoginRequest request);
        Task<UserResponse> RegisterAsync(RegisterRequest request);
        Task<bool> CheckUserEmailAsync(string email);
        Task<AddressDto> GetUserAddressAsync(string email);
        Task<AddressDto> UpdateUserAddressAsync(AddressDto dto, string email);
        Task<UserResponse> GetUserByEmailAsync(string email);
    }
}
