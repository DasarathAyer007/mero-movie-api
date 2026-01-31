using mero_movie_api.Data;
using mero_movie_api.Dto.Request;
using mero_movie_api.Model;
using mero_movie_api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace mero_movie_api.Repositories;

public class UserRepository(AppDbContext context):IUserRepository
{
    private readonly AppDbContext _context=context;
    public async Task<User?> GetUserByID(int id)
    {
        
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public Task<User?> GetUserByUsernameAndPassword(string username, string password)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
    }

    public async Task<User> CreateUser(RegisterUserDto registerUser)
    {
        var newUser = new User
        {
            FirstName = registerUser.FirstName,
            LastName = registerUser.LastName,
            Username = registerUser.Username,
            Email = registerUser.Email,
            Password = registerUser.Password
        };
        await _context.AddAsync(newUser);
        await _context.SaveChangesAsync();
        return newUser ;
    }
}