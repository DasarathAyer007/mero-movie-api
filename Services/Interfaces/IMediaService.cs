using mero_movie_api.Dto.Response;

namespace mero_movie_api.Services.Interfaces;

public interface IMediaService
{
    Task<PaginatedResponse<MediaListResponse>> TrendingList(string type,string timeWindow);

    Task<PaginatedResponse<MediaListResponse>> PopularMovieList(int page=1);
    Task<PaginatedResponse<MediaListResponse>> PopularTvList(int page=1);
    Task<PaginatedResponse<MediaListResponse>> NewMovieList(int page=1);

    Task<PaginatedResponse<MediaListResponse>> NewTvList(int page=1);
}