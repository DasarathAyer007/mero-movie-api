using mero_movie_api.Dto.Request;
using mero_movie_api.Dto.Response;
using mero_movie_api.Model;
using mero_movie_api.Repository.Interface;
using mero_movie_api.Services.Interfaces;

namespace mero_movie_api.Services;

public class WatchListService(IWatchListRepository watchListRepository):IWatchListService
{
    private readonly IWatchListRepository _watchListRepository=watchListRepository;
    public async Task<List<WatchListResponse>> MyWatchList(int userId, WatchStatus? status)
    {
        return  MapToWatchListResponseList(await _watchListRepository.GetMyWatchList(userId, status));
    }

    public async Task<bool> AddMyWatchList(CreateWatchListDto dto,int userId)
    {
        var watchList = new WatchList
        {
            UserId = userId,
            MovieId = dto.MovieId,
            Status = dto.Status,
            Progress = dto.Progress,
            StartedAt = dto.Status == WatchStatus.Watching ? DateTime.UtcNow : null
        };
         await _watchListRepository.AddToWatchList(watchList);
         return true;
    }

    public async Task<bool> DeleteMyWatchList(int id,int userId)
    {
       return  await _watchListRepository.RemoveFromWatchList(id,userId);
    
    }

    public async Task<WatchListResponse> UpdateMyWatchList(UpdateWatchListDto dto, int userId)
    {
        var watchList = await _watchListRepository.GetWatchListById(dto.WatchListId);
        if (watchList == null)
        {
            throw new Exception("WatchList not found");
        }
        watchList.Status = dto.Status ?? watchList.Status;
        watchList.Progress = dto.Progress;
        
        watchList.StartedAt = dto.Status == WatchStatus.Watching ? DateTime.UtcNow : null;

        await _watchListRepository.SaveChangesAsync();
        
        return MapToWatchListResponse(watchList);
        
    }

    public WatchListResponse MapToWatchListResponse(WatchList watchList)
    {
        return new WatchListResponse
        {
            Id = watchList.Id,
            MovieId = watchList.MovieId,
            Status = watchList.Status,
            Progress = watchList.Progress
        };
    }
    
    public List<WatchListResponse> MapToWatchListResponseList(List<WatchList> watchLists)
    {
        return watchLists.Select(w => new WatchListResponse
        {
            Id = w.Id,
            MovieId = w.MovieId,
            Status = w.Status,
            Progress = w.Progress
        }).ToList();
    }

}