using mero_movie_api.Dto.Request;
using mero_movie_api.Dto.Response;
using mero_movie_api.Model;
using mero_movie_api.Repository.Interface;
using mero_movie_api.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace mero_movie_api.Services;

public class AuthService(IUserRepository userRepository,IConfiguration configuration ) :IAuthService
{
    private readonly IUserRepository _userRepository=userRepository;
    private readonly IConfiguration _configuration = configuration;
    public async Task<User?> RegisterUser(RegisterUserDto user)
    {
        return await _userRepository.CreateUser(user);
    }

    public async Task<LoginResponse?> LoginUser(string email, string password)
    {
        var user = await _userRepository.GetUserByEmail(email);

        if (user ==null )
        {
            return null;
        }

        if (user.Password == password)
        {
            return new LoginResponse
            {
                Username = user.Username,
                AccessToken = GenerateJwtToken(user)
            };
        }

        return null;
    }
    
    public string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( _configuration["jwt:Key"] ?? string.Empty));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["jwt:issuer"],
            audience: _configuration["jwt:audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}