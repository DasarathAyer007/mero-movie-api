using mero_movie_api.Model;
using mero_movie_api.Repository.Interface;
using mero_movie_api.Services.Interfaces;

namespace mero_movie_api.Services;

public class WatchListService(IWatchListRepository watchListRepository):IWatchListService
{
    private readonly IWatchListRepository _watchListRepository=watchListRepository;
    public async Task<List<WatchList>> MyWatchList(int userId, WatchStatus? status)
    {
        return await _watchListRepository.GetMyWatchList(userId, status);
    }

    public async Task<bool> AddMyWatchList(WatchList watchList,int userId)
    {
         await _watchListRepository.AddToWatchList(watchList);
         return true;
    }

    public async Task<bool> DeleteMyWatchList(int id,int userId)
    {
        await _watchListRepository.RemoveFromWatchList(id,userId);
        return true;
    }

    public async Task<WatchList>  UpdateMyWatchList(WatchList watchList,int userId)
    {
        return await _watchListRepository.UpdateWatchList(watchList,userId);
    }
}