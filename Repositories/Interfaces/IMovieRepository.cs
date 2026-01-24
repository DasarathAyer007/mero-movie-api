using mero_movie_api.Dto;

namespace mero_movie_api.Repository.Interface;

public interface IMovieRepository
{
    Task<MovieApiResponse> GetTrendingList(string type = "all",string timeWindow="week");
    Task<MovieApiResponse>GetPopularMovie();

    Task<MovieApiResponse> GetPopularTv();

    Task<MovieApiResponse> GetNewMovieList();

    Task<MovieApiResponse> GetNewTvList();


}