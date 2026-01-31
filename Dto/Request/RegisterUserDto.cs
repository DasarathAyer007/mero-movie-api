using System.ComponentModel.DataAnnotations;

namespace mero_movie_api.Dto.Request;

public class RegisterUserDto
{
    [MaxLength(100)] [Required]
    public required string FirstName { get; set; }
    
    [MaxLength(100)]
    [Required]
    public required string LastName { get; set; }
    
    [MaxLength(100)]
    [Required]
    public required string Username { get; set; }
    
    public required  string Password { get; set; }
    
    [MinLength(5),MaxLength(100)]
    [Required]
    public required string Email { get; set; }
}