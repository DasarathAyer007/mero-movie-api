using System.Text.Json.Serialization;

namespace mero_movie_api.Dto;

public class MovieApiDto
{
    [JsonPropertyName("adult")]
    public bool Adult { get; set; }
    
    [JsonPropertyName("id")]
    public int Id { get; init; }
    
    [JsonPropertyName("overview")]
    public string Overview { get; set; }=string.Empty;
    
    [JsonPropertyName("popularity")]
    public double Popularity { get; set; }
    
    [JsonPropertyName("release_date")]  
    public string? ReleaseDate { get; set; }
    
    [JsonPropertyName("first_air_date")]
    public string? FirstAirDate { get; set; }
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("backdrop_path")]
    public string BackdropPath { get; set; }=string.Empty;

    [JsonPropertyName("genre_ids")]
    public List<int> GenreIds { get; set; }=new List<int>();

    [JsonPropertyName("original_language")]
    public string OriginalLanguage { get; set; }=string.Empty;

    [JsonPropertyName("poster_path")]
    public string PosterPath { get; set; }=string.Empty;

    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; set; }

    [JsonPropertyName("vote_count")]
    public int VoteCount { get; set; }
    
    [JsonPropertyName("media_type")]
    public string MediaType { get; set; }=string.Empty;
    
    [JsonPropertyName("number_of_episodes")]
    public int? TotalEpisodes { get; set; }
    
    [JsonPropertyName("number_of_seasons")]
    public int? TotalSeasons { get; set; }

}