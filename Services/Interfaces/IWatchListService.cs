using mero_movie_api.Dto.Request;
using mero_movie_api.Dto.Response;
using mero_movie_api.Model;

namespace mero_movie_api.Services.Interfaces;

public interface IWatchListService
{
    Task<List<WatchListResponse>> MyWatchList(int userId, WatchStatus? status);

    Task<List<int>> WatchListIds(int userId);

    Task<List<WatchListResponse>> WatchListsMedia(int userId);

    Task<bool> AddMyWatchList(CreateWatchListDto watchList, int userId);

    Task<bool> DeleteMyWatchList(int id, int userId);

    Task<WatchListResponse> UpdateMyWatchList(UpdateWatchListDto watchList, int userId);
}