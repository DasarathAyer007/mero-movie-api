using System.Net.Http.Headers;
using mero_movie_api.Dto;
using mero_movie_api.Repository.Interface;

namespace mero_movie_api.Repositories;

public class MovieRepository:IMovieRepository
{
    private readonly string _baseUrl="https://api.themoviedb.org/3/";
    private readonly HttpClient _httpClient;

    public MovieRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_baseUrl);
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer",
                "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI4NmE4NTI2YzhkNWFhMzhmOTM2ZGQ1ODE0YjJlZThhNiIsIm5iZiI6MTc2ODQ3OTY4OS45ODIsInN1YiI6IjY5NjhkYmM5MDkxN2Y3ZmExZDY4YTQ1YyIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.ggk_CZECVfmFOMr9CY-pXpvz9ClJk0VvCCnRTBWuDSg");
    }

    public async Task<MovieApiResponse> GetTrendingList(string type = "all",string timeWindow="week")
    {
        type = Normalize(type, new[] { "all", "movie", "tv" }, "all");
        timeWindow = Normalize(timeWindow, new[] { "day", "week" }, "week");

        return await FetchListAsync($"trending/{type}/{timeWindow}");
    }

    public async Task<MovieApiResponse> GetPopularMovie()
    {
        return await FetchListAsync("/movie/popular");
    }

    public async Task<MovieApiResponse> GetPopularTv()
    {
        return await FetchListAsync("/tv/popular");
    }

    public async Task<MovieApiResponse>GetNewMovieList()
    {
        return   await FetchListAsync("trending/movie/now_playing");
    }
    
    public async Task<MovieApiResponse> GetNewTvList()
    {
        return await FetchListAsync("trending//tv/on_the_air");
    }
    
    private static string Normalize(string? value, string[] allowed, string fallback)
    {
        value = value?.ToLower() ?? fallback;
        return allowed.Contains(value) ? value : fallback;
    }

    
    private async Task<MovieApiResponse> FetchListAsync(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync();

            throw new HttpRequestException(
                $"TMDB API failed ({(int)response.StatusCode}): {errorBody}"
            );
        }

        var data = await response.Content.ReadFromJsonAsync<MovieApiResponse>();
        if (data?.Results != null)
        {
            foreach (var x in data.Results)
            {
                if (string.IsNullOrEmpty(x.Title))
                    x.Title = x.Name;

                if (string.IsNullOrEmpty(x.ReleaseDate))
                    x.ReleaseDate = x.FirstAirDate;
            }
        }
        
        return data ?? new MovieApiResponse
        {
            Page = 1,
            Results = new List<MovieApiDto>()
        };
        
    }
    
    
    

}