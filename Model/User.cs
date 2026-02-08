using System.ComponentModel.DataAnnotations;

namespace mero_movie_api.Model;

public class User : BaseEntity
{
    public int Id { get; init; }

    [MaxLength(100)] [Required] public required string FirstName { get; set; }

    [MaxLength(100)] [Required] public required string LastName { get; set; }

    [MaxLength(100)] [Required] public required string Username { get; set; }

    [MinLength(5), MaxLength(100)]
    [Required]
    public required string Email { get; set; }

    [MinLength(5), MaxLength(100)] public required string Password { get; set; }

    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    public ICollection<WatchList> WatchLists { get; set; } = new List<WatchList>();

    public ICollection<Collection> Collections { get; set; } = new List<Collection>();
}