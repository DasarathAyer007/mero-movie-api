using System.Security.Claims;

namespace mero_movie_api.Shared;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _accessor;

    public CurrentUser(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public int UserId
    {
        get
        {
            var user = _accessor.HttpContext?.User
                       ?? throw new UnauthorizedAccessException("No user context available");

            var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(idClaim))
                throw new UnauthorizedAccessException("User ID claim missing");

            if (!int.TryParse(idClaim, out var id))
                throw new UnauthorizedAccessException($"Invalid User ID in token: {idClaim}");

            return id;
        }
    }

    public string? UserName => _accessor.HttpContext?.User?.Identity?.Name;
    public string? Email => _accessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
}