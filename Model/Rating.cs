using System.ComponentModel.DataAnnotations;

namespace mero_movie_api.Model;

public class Rating : BaseEntity
{
    [Required] public int Id { get; init; }

    [Required] public int UserId { get; init; }
    public User User { get; set; } = null!;

    [Required] public int MediaId { get; init; }
    public Media Media { get; set; } = null!;


    [Range(1.0, 10.0)] public decimal MediaRating { get; set; }
}