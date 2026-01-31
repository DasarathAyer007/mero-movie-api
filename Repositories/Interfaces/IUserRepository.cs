using mero_movie_api.Dto.Request;
using mero_movie_api.Model;

namespace mero_movie_api.Repository.Interface;

public interface IUserRepository
{
    Task<User?> GetUserByID(int id);
    
    Task<User?> GetUserByEmail(string email);
    
    Task<User?> GetUserByUsername(string username);
    
    Task<User?> GetUserByUsernameAndPassword(string username, string password);
    
    Task<User> CreateUser(RegisterUserDto registerUser);
}