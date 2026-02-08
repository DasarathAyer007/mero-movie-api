using mero_movie_api.Dto.Request;
using mero_movie_api.Dto.Response;
using mero_movie_api.Model;
using mero_movie_api.Repository.Interface;
using mero_movie_api.Services.Interfaces;

namespace mero_movie_api.Services;

public class WatchListService(IWatchListRepository watchListRepository, IMovieRepository movieRepository)
    : IWatchListService
{
    private readonly IWatchListRepository _watchListRepository = watchListRepository;
    private readonly IMovieRepository _movieRepository = movieRepository;

    public async Task<List<WatchListResponse>> MyWatchList(int userId, WatchStatus? status)
    {
        var watchList = await _watchListRepository.GetMyWatchList(userId, status);

        // watchList.Select(w =>
        // {
        //     _movieRepository.getMovi
        // })
        return MapToWatchListResponseList(await _watchListRepository.GetMyWatchList(userId, status));
    }

    public async Task<List<int>> WatchListIds(int userId)
    {
        return await _watchListRepository.GetMyWatchListIds(userId);
    }

    public async Task<List<WatchListResponse>> WatchListsMedia(int userId)
    {
        return await _watchListRepository.GetMyWatchListsMedia(userId);
    }

    public async Task<bool> AddMyWatchList(CreateWatchListDto dto, int userId)
    {
        int? movieId = null;
        movieId = await _movieRepository.DoesMovieExist(dto.MovieId, dto.Mediatype);
        if (movieId == null)
        {
            movieId = await _movieRepository.AddFromApiToDb(dto.MovieId, dto.Mediatype);
        }

        if (movieId == null)
        {
            throw new Exception("Watch list could not be added");
        }

        var watchList = new WatchList
        {
            UserId = userId,
            MediaId = movieId.Value,
            Status = dto.Status,
            Progress = dto.Progress,
            StartedAt = dto.Status == WatchStatus.Watching ? DateTime.UtcNow : null
        };
        await _watchListRepository.AddToWatchList(watchList);
        return true;
    }

    public async Task<bool> DeleteMyWatchList(int id, int userId)
    {
        return await _watchListRepository.RemoveFromWatchList(id, userId);
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
            MovieId = watchList.MediaId,
            Status = watchList.Status,
            Progress = watchList.Progress
        };
    }

    public List<WatchListResponse> MapToWatchListResponseList(List<WatchList> watchLists)
    {
        return watchLists.Select(w => new WatchListResponse
        {
            Id = w.Id,
            MovieId = w.MediaId,
            Status = w.Status,
            Progress = w.Progress
        }).ToList();
    }
}