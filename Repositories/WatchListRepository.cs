using mero_movie_api.Data;
using mero_movie_api.Dto.Response;
using mero_movie_api.Model;
using mero_movie_api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace mero_movie_api.Repositories;

public class WatchListRepository(AppDbContext context) : IWatchListRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<WatchList>> GetMyWatchList(int userId, WatchStatus? status)
    {
        IQueryable<WatchList> query = _context.WatchLists.Where(w => w.UserId == userId);

        if (status.HasValue)
        {
            query = query.Where(s => s.Status == status.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<List<int>> GetMyWatchListIds(int userId)
    {
        return await _context.WatchLists
            .Where(w => w.UserId == userId)
            .Select(w => w.Media.ExternalMovieId)
            .ToListAsync();
    }

    public async Task<List<WatchListResponse>> GetMyWatchListsMedia(int userId)
    {
        return await _context.WatchLists
            .Where(w => w.UserId == userId)
            .Include(w => w.Media)
            .Select(w => new WatchListResponse
            {
                Id = w.Id,
                MovieId = w.MediaId,
                Progress = w.Progress,
                Status = w.Status,
                TotalEpisodes = w.Media.TvShow != null
                    ? w.Media.TvShow.Episode
                    : 1,
                Media = new MediaListResponse
                {
                    Id = w.Media.Id,
                    Title = w.Media.Title,
                    Overview = w.Media.Overview,
                    ReleaseDate = w.Media.ReleaseDate,
                    Popularity = w.Media.Popularity,
                    Adult = w.Media.Adult,
                    GenreIds = w.Media.GenreIds.ToList(),
                    OriginalLanguage = w.Media.OriginalLanguage,
                    PosterPath = w.Media.PosterPath,
                    MediaType = w.Media.MediaType,
                    BackdropPath = w.Media.BackdropPath,
                }
            })
            .ToListAsync();
    }


    public async Task AddToWatchList(WatchList watchList)
    {
        await _context.WatchLists.AddAsync(watchList);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> RemoveFromWatchList(int id, int userId)
    {
        var removeWatchList = await _context.WatchLists
            .FirstOrDefaultAsync(w => w.UserId == userId
                                      && w.Media.ExternalMovieId == id);
        if (removeWatchList != null)
        {
            _context.WatchLists.Remove(removeWatchList);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<WatchList> UpdateWatchList(WatchList watchList, int userId)
    {
        var existing = await _context.WatchLists.FirstOrDefaultAsync(w => w.Id == watchList.Id);
        if (existing == null)
        {
            throw new Exception("WatchList not found");
        }

        existing.Status = watchList.Status;
        existing.Progress = watchList.Progress;
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<WatchList> GetWatchListById(int id)
    {
        return await _context.WatchLists.FirstOrDefaultAsync(w => w.Id == id) ?? new WatchList();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}