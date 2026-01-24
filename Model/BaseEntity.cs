namespace mero_movie_api.Model;

public class BaseEntity
{
    public DateTime CreatedOn { get; init; }
    public DateTime ModifiedOn { get; set; }
}