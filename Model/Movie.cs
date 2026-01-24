using System.ComponentModel.DataAnnotations;

namespace mero_movie_api.Model;

public class Movie : BaseEntity
{
    public int Id { get; init; }

    [Required] public int ExternalMovieId { get; set; }

    [MaxLength(200)] public string? Title { get; set; }


    public ICollection<Collection> Collection { get; set; } = new List<Collection>();
}