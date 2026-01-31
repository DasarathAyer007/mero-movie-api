using mero_movie_api.Dto.Request;
using mero_movie_api.Dto.Response;
using mero_movie_api.Model;
using mero_movie_api.Services.Interfaces;
using mero_movie_api.Shared;
using Microsoft.AspNetCore.Mvc;

namespace mero_movie_api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(RegisterUserDto registerUser)
    {
        var result = await _authService.RegisterUser(registerUser);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<LoginResponse?>>> Login(LoginRequestDto loginRequest)
    {
        var response = await _authService.LoginUser(loginRequest.Email, loginRequest.Password);
        if (response == null)
        {
            return Unauthorized(ApiResponse<bool>.ErrorResponse("Invalid username or password"));
        }
        else
        {
            return Ok(ApiResponse<LoginResponse?>.SuccessResponse(response, "Login Successful"));
        }
    }
}