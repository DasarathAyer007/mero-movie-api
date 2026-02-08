using System.ComponentModel.DataAnnotations;

namespace mero_movie_api.Model;

public class Collection : BaseEntity
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public User User { get; set; } = null!;

    [Required] [MaxLength(100)] public required string Name { get; set; }

    [MaxLength(100)] public string? Description { get; set; }

    public ICollection<Media> Media { get; set; } = new List<Media>();
}