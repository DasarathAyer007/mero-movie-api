using mero_movie_api.Data;
using mero_movie_api.Model;
using mero_movie_api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace mero_movie_api.Repositories;

public class WatchListRepository(AppDbContext context):IWatchListRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<WatchList>> GetMyWatchList(int userId,WatchStatus? status)
    {
        IQueryable<WatchList> query = _context.WatchLists.Where(w => w.UserId == userId);

        if (status.HasValue)
        {
            query = query.Where(s => s.Status == status.Value);
        }

        return await query.ToListAsync();
    }
    
    public async Task AddToWatchList(WatchList watchList)
    {
        var movie = await _context.Movies
            .FirstOrDefaultAsync(m => m.ExternalMovieId == watchList.MovieId);
        
        if (movie == null)
        {
            movie = new Movie
            {
                ExternalMovieId = watchList.MovieId
            };

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync(); 
        }
        
        watchList.MovieId = movie.Id; 
        
        await _context.WatchLists.AddAsync(watchList);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> RemoveFromWatchList(int id, int userId)
    {
        var removeWatchList = await _context.WatchLists.FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);
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
        var existing=await  _context.WatchLists.FirstOrDefaultAsync(w => w.Id == watchList.Id);
        if (existing == null)
        {
            throw new Exception("WatchList not found");
        }
        existing.Status=watchList.Status;
        existing.Progress=watchList.Progress;
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