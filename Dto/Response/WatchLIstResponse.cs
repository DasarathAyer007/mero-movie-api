using mero_movie_api.Model;

namespace mero_movie_api.Dto.Response;

public class WatchListResponse
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public int? Progress { get; set; }
    public int? TotalEpisodes { get; set; }
    public WatchStatus Status { get; set; }
    
    public string StatusName => Status.ToString();
    
    public MediaListResponse? Media { get; set; }
}