using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction.IService;
using Shared.Dtos.AuthenticationDto;
using Shared.Setting;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    internal class AuthenticationService(UserManager<ApplicationUser> userManager, IOptions<JWT> jwtOptions ,IMapper _mapper)
                : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly JWT _jwtOptions = jwtOptions.Value;

        public async Task<bool> CheckUserEmailAsync(string email)
            => await _userManager.FindByEmailAsync(email) != null;
        public async Task<UserResponse> GetUserByEmailAsync(string email)
        {
            var  user = await _userManager.FindByEmailAsync(email) ??
                throw new UserNotFoundException(email);

            return new(user.Email!,user.DisplayName,await CreateJWTToken(user));
        }

        public async Task<AddressDto> GetUserAddressAsync(string email)
        {
            var user = await _userManager.Users
                .Include(u=>u.Address)
                .FirstOrDefaultAsync(u=>u.Email == email)
                ?? throw new UserNotFoundException(email);
             var address = user.Address;
            if(address is not null) return _mapper.Map<AddressDto>(address);

            throw new AddressNotFoundException(user.UserName!);
        }

        public async Task<AddressDto> UpdateUserAddressAsync(AddressDto dto, string email)
        {
            var user = await _userManager.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == email)
                ?? throw new UserNotFoundException(email);

            var address = user.Address;
            if (address is not null)
            {
                address.City = dto.City;
                address.Country = dto.Country;
                address.Street = dto.Street;
                address.FirstName = dto.FirstName;
                address.LastName = dto.LastName;
            }
            else
            {
                address = _mapper.Map<Address>(dto);
            }

            await _userManager.UpdateAsync(user);
            return _mapper.Map<AddressDto>(address);
        }

        public async Task<UserResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email) ??
                throw new UserNotFoundException(request.Email);

            var isValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (isValid) return new UserResponse(user.Email!, user.DisplayName, await CreateJWTToken(user));

            throw new UnauthorizedException(request.Email);
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                Email = request.Email,
                DisplayName = request.DispalyName,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded) return new UserResponse(user.Email!, user.DisplayName,await CreateJWTToken(user));
            var errors = result.Errors.Select(e => e.Description).ToList();
            throw new BadRequestException(errors);
        }      

        private async Task<string> CreateJWTToken(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
    {
        new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
        new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
    };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwtOptions.DurationInDays),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
