using mero_movie_api.Dto.Request;
using mero_movie_api.Dto.Response;
using mero_movie_api.Model;

namespace mero_movie_api.Services.Interfaces;

public interface IAuthService
{
    Task<User?> RegisterUser(RegisterUserDto user);
    Task<LoginResponse?> LoginUser(string email, string password);
}