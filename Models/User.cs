using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuthExample.Models
{
  public class User
  {
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    [JsonIgnore]
    public string HashedPassword { get; set; }

    public string Email { get; set; }
    public string FullName { get; set; }
    public string ProfileUrl { get; set; }

    public List<UserFavorite> UserFavorite { get; set; } = new List<UserFavorite>();
  }
}