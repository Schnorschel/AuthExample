using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AuthExample.Models
{
  public class UserFavorite
  {
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public int FavoriteId { get; set; }

    [JsonIgnore]
    public User User { get; set; }
    public Favorite Favorite { get; set; }

    // [JsonIgnore]
    // public List<User> Users { get; set; } = new List<User>();
    // public List<Favorite> Favorites { get; set; } = new List<Favorite>();
  }
}