using mero_movie_api.Dto;
using mero_movie_api.Dto.Response;
using mero_movie_api.Repository.Interface;
using mero_movie_api.Services.Interfaces;

namespace mero_movie_api.Services;

public class MediaService(IMovieRepository movieRepository) : IMediaService
{
    private readonly IMovieRepository _movieRepository=movieRepository;
    
    public async Task<PaginatedResponse<MediaListResponse>> TrendingList(string type, string timeWindow)
    {
        var response = await _movieRepository.GetTrendingList(type, timeWindow);
        return  MapToMediaListResponse(response);

    }

    public async Task<PaginatedResponse<MediaListResponse>> PopularMovieList(int page=1)
    {
        var response = await _movieRepository.GetPopularMovie();
        return  MapToMediaListResponse(response);

    }
    public async Task<PaginatedResponse<MediaListResponse>> PopularTvList(int page=1)
    {
        var response = await _movieRepository.GetPopularTv();
        return  MapToMediaListResponse(response);

    }
    public async Task<PaginatedResponse<MediaListResponse>> NewMovieList(int page=1)
    {
        var response = await _movieRepository.GetNewMovieList();
        return  MapToMediaListResponse(response);

    }
    public async Task<PaginatedResponse<MediaListResponse>> NewTvList(int page=1)
    {
        var response = await _movieRepository.GetNewTvList();
        return  MapToMediaListResponse(response);

    }
    
    public PaginatedResponse<MediaListResponse> MapToMediaListResponse(MovieApiResponse movies )
    {
        return new PaginatedResponse<MediaListResponse>
        {
            Page = movies.Page,
            Results  = movies.Results!=null ? movies.Results.Select(x => new MediaListResponse
            {
                Id = x.Id,
                Overview = x.Overview,
                Popularity = x.Popularity,
                ReleaseDate = x.ReleaseDate,
                Title = x.Title,
                GenreIds = x.GenreIds,
                OriginalLanguage = x.OriginalLanguage,
                PosterPath = x.PosterPath,
                VoteAverage = x.VoteAverage,
                VoteCount = x.VoteCount,
                MediaType = x.MediaType
            } ):new List<MediaListResponse>()

        };
    }
    
    
}