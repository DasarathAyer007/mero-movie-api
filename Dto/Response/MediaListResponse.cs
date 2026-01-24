using System.Text.Json.Serialization;

namespace mero_movie_api.Dto.Response;

public class MediaListResponse
{
    public bool Adult { get; set; }

    public int Id { get; set; }
    
    public string? Overview { get; set; }=string.Empty;
    
    public double Popularity { get; set; }

    public string? ReleaseDate { get; set; }

    public string? Title { get; set; }

    public List<int> GenreIds { get; set; } = new List<int>();

    public string OriginalLanguage { get; set; }=string.Empty;

    public string PosterPath { get; set; }=string.Empty;

    public double VoteAverage { get; set; }

    public int VoteCount { get; set; }
    
    public string MediaType { get; set; }=string.Empty;
}