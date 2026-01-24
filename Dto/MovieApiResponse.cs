using System.Text.Json.Serialization;

namespace mero_movie_api.Dto;

public class MovieApiResponse
{
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("results")]
    public List<MovieApiDto>? Results { get; set; }
}