namespace mero_movie_api.Shared;

public interface ICurrentUser
{
    int UserId { get; }
    string? UserName { get; }
    string? Email { get; }
}