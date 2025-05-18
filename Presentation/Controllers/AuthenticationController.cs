using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;
using ServiceAbstraction.IService;
using Shared.Dtos.AuthenticationDto;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

public class AuthenticationController(IServiceManager serviceManager) : APIController
{
    private readonly IServiceManager _serviceManager = serviceManager;

    [HttpPost("login")]
    public async Task<ActionResult<UserResponse>> Login([FromQuery] LoginRequest request)
    {
        return Ok(await _serviceManager.AuthenticationService.LoginAsync(request));
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> Register([FromQuery] RegisterRequest request)
    {
        return Ok(await _serviceManager.AuthenticationService.RegisterAsync(request));
    }
    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckUserEmailAsync(string email)
        => Ok(await serviceManager.AuthenticationService.CheckUserEmailAsync(email));

    [HttpGet]
    public async Task<ActionResult<UserResponse>> GetCurrentUserByEmailAsync()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _serviceManager.AuthenticationService.GetUserByEmailAsync(email!);
        return Ok(user);
    }
    [HttpGet("Address")]
    public async Task<ActionResult<AddressDto>> GetUserAddressAsync()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var address = await _serviceManager.AuthenticationService.GetUserAddressAsync(email!);
        return Ok(address);
    }
    [HttpPut("Address")]
    public async Task<ActionResult<AddressDto>> UpdateUserAddressAsync([FromQuery] AddressDto addressDto)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var updatedAddress = await _serviceManager.AuthenticationService.UpdateUserAddressAsync(addressDto, email!);
        return Ok(updatedAddress);
    }
}
