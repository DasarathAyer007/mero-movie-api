using System.ComponentModel.DataAnnotations;

namespace mero_movie_api.Model;

public class Media : BaseEntity
{
    public int Id { get; init; }

    [Required] public int ExternalMovieId { get; set; }

    [MaxLength(200)] public string? Title { get; set; }
    public bool Adult { get; set; }

    [MaxLength(800)] public string? Overview { get; set; } = string.Empty;

    public double Popularity { get; set; }

    [MaxLength(50)] public string? ReleaseDate { get; set; }

    public List<int> GenreIds { get; set; } = new();

    [MaxLength(20)] public string OriginalLanguage { get; set; } = string.Empty;

    [MaxLength(200)] public string PosterPath { get; set; } = string.Empty;

    [MaxLength(50)] public string MediaType { get; set; } = string.Empty;

    [MaxLength(200)] public string BackdropPath { get; set; } = string.Empty;

    public ICollection<Collection> Collection { get; set; } = new List<Collection>();

    public TvShowDetail? TvShow { get; set; }

    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}