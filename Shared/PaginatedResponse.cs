namespace mero_movie_api.Dto.Response;

public class PaginatedResponse<T>
{
    public int Page { get; set; } 
    
    public int? TotalPage { get; set; }
    
    public int? TotalCount { get; set; }
    
    public string? Next { get; set; }
    public string? Prev { get; set; }
    
    public  IEnumerable<T> Results { get; set; }=new List<T>();
}