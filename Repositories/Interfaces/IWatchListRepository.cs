using mero_movie_api.Dto.Response;
using mero_movie_api.Model;

namespace mero_movie_api.Repository.Interface;

public interface IWatchListRepository
{
    Task<List<WatchList>> GetMyWatchList(int userId, WatchStatus? status);

    Task<List<int>> GetMyWatchListIds(int userId);
    Task<List<WatchListResponse>> GetMyWatchListsMedia(int userId);

    Task AddToWatchList(WatchList watchList);

    Task<bool> RemoveFromWatchList(int id, int userId);

    Task<WatchList> UpdateWatchList(WatchList watchList, int userId);

    Task<WatchList> GetWatchListById(int id);

    Task SaveChangesAsync();
}