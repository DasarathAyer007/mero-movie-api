using mero_movie_api.Model;

namespace mero_movie_api.Dto.Request;

public class CreateWatchListDto
{
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public WatchStatus Status { get; set; } = WatchStatus.PlanToWatch;
    public int? Progress { get; set; }
}