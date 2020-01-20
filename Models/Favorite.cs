using System.ComponentModel.DataAnnotations;

namespace AuthExample.Models
{
  public class Favorite
  {
    public int Id { get; set; }
    [Required]
    public int fdcId { get; set; }
    public string description { get; set; }
    public string gtinUpc { get; set; }
    public string brandOwner { get; set; }
    public string ingredients { get; set; }
    public string additionalDescriptions { get; set; }
  }
}