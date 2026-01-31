namespace mero_movie_api.Model;

public class BaseEntity
{
    public DateTime CreatedOn { get; init; }=DateTime.UtcNow;
    public DateTime ModifiedOn { get; set; }=DateTime.UtcNow;
}