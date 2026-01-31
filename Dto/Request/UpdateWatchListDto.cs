using mero_movie_api.Model;

namespace mero_movie_api.Dto.Request;

public class UpdateWatchListDto
{
    public int WatchListId { get; set; }
    public WatchStatus? Status { get; set; }
    public int? Progress { get; set; }
}