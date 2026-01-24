using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mero_movie_api.Model;

public class Review:BaseEntity
{
    [Required]
    public int Id { get; init; }
    
    [Required]
    public int UserId { get; init; }
    public User User { get; set; } = null!;
    
    [Required]
    public int MovieId { get; init; }
    public Movie Movie { get; set; } = null!;

    [Column(TypeName = "text")] 
    public string ReviewText { get; set; } = null!;
    
    
}