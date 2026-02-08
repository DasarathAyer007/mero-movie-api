using mero_movie_api.Dto;
using mero_movie_api.Model;

namespace mero_movie_api.Repository.Interface;

public interface IMovieRepository
{
    Task<MovieApiResponse> GetTrendingList(string type = "all", string timeWindow = "week");
    Task<MovieApiResponse> GetPopularMovie();
    
    Task<MovieApiResponse> GetPopularTv();

    Task<MovieApiResponse> GetNewMovieList();

    Task<MovieApiResponse> GetNewTvList();
    Task<Media?> GetMovieById(int id);

    Task<int?> DoesMovieExist(int id, string mediaType);

    Task<int?> AddFromApiToDb(int externalId, string mediaType);
}