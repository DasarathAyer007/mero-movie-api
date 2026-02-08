using System.Net.Http.Headers;
using mero_movie_api.Data;
using mero_movie_api.Dto;
using mero_movie_api.Model;
using mero_movie_api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace mero_movie_api.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly string _baseUrl = "https://api.themoviedb.org/3/";
    private readonly HttpClient _httpClient;
    private readonly AppDbContext _context;

    public MovieRepository(HttpClient httpClient, AppDbContext context)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_baseUrl);
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer",
                "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI4NmE4NTI2YzhkNWFhMzhmOTM2ZGQ1ODE0YjJlZThhNiIsIm5iZiI6MTc2ODQ3OTY4OS45ODIsInN1YiI6IjY5NjhkYmM5MDkxN2Y3ZmExZDY4YTQ1YyIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.ggk_CZECVfmFOMr9CY-pXpvz9ClJk0VvCCnRTBWuDSg");
        _context = context;
    }

    public async Task<MovieApiResponse> GetTrendingList(string type = "all", string timeWindow = "week")
    {
        type = Normalize(type, new[] { "all", "movie", "tv" }, "all");
        timeWindow = Normalize(timeWindow, new[] { "day", "week" }, "week");
        string? mediaType = type == "all" ? null : type;

        return await FetchListAsync($"trending/{type}/{timeWindow}", mediaType);
    }


    public async Task<Media?> GetMovieById(int id)
    {
        return await _context.Media.FindAsync(id);
    }

    public async Task<int?> DoesMovieExist(int id, string mediaType)
    {
        return await _context.Media.Where(m => m.ExternalMovieId == id && m.MediaType == mediaType)
            .Select(m => (int?)m.Id).FirstOrDefaultAsync();
    }

    public async Task<int?> AddFromApiToDb(int externalId, string mediaType)
    {
        if (mediaType == "movie" || mediaType == "tv")
        {
            MovieApiDto? response = await FetchSingleAsync($"{mediaType}/{externalId}", mediaType);

            if (response != null)
            {
                Media movie = MapApiToModel(response, mediaType);
                await _context.Media.AddAsync(movie);
                await _context.SaveChangesAsync();
                return movie.Id;
            }
        }

        return null;
    }


    public async Task<MovieApiResponse> GetPopularMovie()
    {
        return await FetchListAsync("movie/popular", "movie");
    }

    public async Task<MovieApiResponse> GetPopularTv()
    {
        return await FetchListAsync("tv/popular", "tv");
    }

    public async Task<MovieApiResponse> GetNewMovieList()
    {
        return await FetchListAsync("movie/now_playing", "movie");
    }

    public async Task<MovieApiResponse> GetNewTvList()
    {
        return await FetchListAsync("tv/on_the_air", "tv");
    }


    private static string Normalize(string? value, string[] allowed, string fallback)
    {
        value = value?.ToLower() ?? fallback;
        return allowed.Contains(value) ? value : fallback;
    }


    private async Task<MovieApiResponse> FetchListAsync(string endpoint, string? mediaType = null)
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

                if (string.IsNullOrEmpty(x.MediaType) && !string.IsNullOrEmpty(mediaType))
                    x.MediaType = mediaType;
            }
        }

        return data ?? new MovieApiResponse
        {
            Page = 1,
            Results = new List<MovieApiDto>()
        };
    }

    private async Task<MovieApiDto?> FetchSingleAsync(string endpoint, string? mediaType = null)
    {
        try
        {
            var response = await _httpClient.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null; // movie/TV not found

                var errorBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(
                    $"TMDB API failed ({(int)response.StatusCode}): {errorBody}"
                );
            }

            var data = await response.Content.ReadFromJsonAsync<MovieApiDto>();

            if (data != null)
            {
                if (string.IsNullOrEmpty(data.Title))
                    data.Title = data.Name;

                if (string.IsNullOrEmpty(data.ReleaseDate))
                    data.ReleaseDate = data.FirstAirDate;

                if (string.IsNullOrEmpty(data.MediaType) && !string.IsNullOrEmpty(mediaType))
                    data.MediaType = mediaType;
            }

            return data;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching {endpoint}: {ex.Message}");
            return null;
        }
    }


    private Media MapApiToModel(MovieApiDto media, string mediaType)
    {
        var mapped = new Media
        {
            ExternalMovieId = media.Id,
            Title = media.Title,
            Adult = media.Adult,
            Overview = media.Overview,
            Popularity = media.Popularity,
            ReleaseDate = media.ReleaseDate,
            GenreIds = media.GenreIds,
            OriginalLanguage = media.OriginalLanguage,
            PosterPath = media.PosterPath,
            MediaType = media.MediaType,
            BackdropPath = media.BackdropPath,
        };

        if (mediaType == "tv")
        {
            mapped.TvShow = new TvShowDetail
            {
                Media = mapped,
                Episode = media.TotalEpisodes ?? 1,
                Season = media.TotalSeasons ?? 1
            };
        }

        return mapped;
    }
}