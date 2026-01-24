using mero_movie_api.Model;

namespace mero_movie_api.Services.Interfaces;

public interface IWatchListService
{
    Task<List<WatchList>> MyWatchList(int userId, WatchStatus? status);

    Task<bool> AddMyWatchList(WatchList watchList,int userId);
    
    Task<bool> DeleteMyWatchList(int id, int userId);

    Task<WatchList> UpdateMyWatchList(WatchList watchList, int userId);
}