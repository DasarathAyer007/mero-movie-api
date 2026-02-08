namespace mero_movie_api.Model;

public class TvShowDetail : BaseEntity
{
    public int Id { get; set; }
    public int Season { get; set; }
    public int Episode { get; set; }

    public int MediaId { get; set; }
    public Media Media { get; set; } = null!;
}