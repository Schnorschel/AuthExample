using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthExample.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthExample.Services
{
  public class AuthService
  {

    // private readonly string KEY;

    // public AuthService(IConfiguration configuration)
    // {
    //   KEY = configuration["JWT-KEY"];
    // }

    public AuthenticatedData CreateToken(Models.User user)
    {
      var expirationTime = DateTime.UtcNow.AddHours(10);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new[]
        {
            new Claim("id", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Username)

      }),
        Expires = expirationTime,
        SigningCredentials = new SigningCredentials(
               new SymmetricSecurityKey(Encoding.ASCII.GetBytes("bRhYJRlZvBj2vW4MrV5HVdPgIE6VMtCFB0kTtJ1m")),
              SecurityAlgorithms.HmacSha256Signature
          )
      };
      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
      return new AuthenticatedData
      {
        FullName = user.FullName,
        Token = token,
        UserId = user.Id,
        Username = user.Username,
        ExpirationTime = expirationTime
      }; ;
    }
  }
}