using System.ComponentModel.DataAnnotations;

namespace AuthExample.Models
{
  public class User
  {
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string HashedPassword { get; set; }

    public string Email { get; set; }
    public string FullName { get; set; }
    public string ProfileUrl { get; set; }
  }
}