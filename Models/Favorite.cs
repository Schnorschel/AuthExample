using System.ComponentModel.DataAnnotations;

namespace AuthExample.Models
{
  public class Favorite
  {
    public int Id { get; set; }
    [Required]
    public int fdcId { get; set; }
  }
}