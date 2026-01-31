namespace mero_movie_api.Dto.Request;

public class LoginRequestDto
{
    public string Username { get; set; } =String.Empty;
    public required string Password {get; set;}
    public string Email { get; set; } =String.Empty;
}