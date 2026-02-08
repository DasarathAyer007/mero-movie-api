namespace mero_movie_api.Model;

public class WatchList : BaseEntity
{
    public int Id { get; init; }

    public int UserId { get; init; }
    public User User { get; set; } = null!;

    public int MediaId { get; set; }
    public Media Media { get; set; } = null!;

    public WatchStatus Status { get; set; } = WatchStatus.PlanToWatch;

    public int? Progress { get; set; }

    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}