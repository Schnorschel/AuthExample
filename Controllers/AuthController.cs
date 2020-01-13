using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthExample.Models;
using AuthExample.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthExample.Services;
using Microsoft.Extensions.Configuration;

namespace AuthExample.Controllers
{
  [Route("auth")]
  [ApiController]
  public class AuthController : ControllerBase
  {



    private readonly DatabaseContext db;

    // private readonly IConfiguration configuration;

    public AuthController(DatabaseContext context) // , IConfiguration config)
    {
      this.db = context;
      // this.configuration = config;
    }

    [HttpPost("signup")]
    public async Task<ActionResult> SignUpUser(NewUserModel userData)
    {
      if (userData.Email.Contains("@gmail.com"))
      {
        return BadRequest(new { Message = "Sorry, no google accounts" });
      }

      var existingUser = await this.db.Users.FirstOrDefaultAsync(f => f.Username == userData.Username);
      if (existingUser != null)
      {
        return BadRequest(new { Message = "user already exists" });
      }

      var existingUserEmail = await this.db.Users.FirstOrDefaultAsync(f => f.Email == userData.Email);
      if (existingUserEmail != null)
      {
        return BadRequest(new { Message = "email address is already exists" });
      }

      var user = new User
      {
        Email = userData.Email,
        FullName = userData.FullName,
        Username = userData.Username,
        HashedPassword = ""
      };
      // hash the password

      var hashed = new PasswordHasher<User>().HashPassword(user, userData.Password);
      user.HashedPassword = hashed;

      this.db.Users.Add(user);
      await this.db.SaveChangesAsync();
      // var rv = new AuthService(this.configuration).CreateToken(user);
      var rv = new AuthService().CreateToken(user);
      return Ok(rv);
    }


    [HttpPost("login")]
    public async Task<ActionResult> LoginUser(LoginViewModel loginData)
    {
      var user = await this.db.Users.FirstOrDefaultAsync(f => f.Username == loginData.Username);
      if (user == null)
      {
        return BadRequest(new { Message = "User does not exist" });
      }

      var verificationResult = new PasswordHasher<User>().VerifyHashedPassword(user, user.HashedPassword, loginData.Password);

      if (verificationResult == PasswordVerificationResult.Success)
      {
        //        var rv = new AuthService(this.configuration).CreateToken(user);
        var rv = new AuthService().CreateToken(user);
        return Ok(rv);
      }
      else
      {
        return BadRequest(new { message = "Wrong password" });
      }
    }

  }
}