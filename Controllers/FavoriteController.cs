using Microsoft.AspNetCore.Mvc;
using AuthExample.Models;
using AuthExample.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AuthExample.Controllers
{
  // [Route("api/[controller]")]
  [Route("api")]
  [ApiController]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class FavoriteController : ControllerBase
  {
    private readonly DatabaseContext context;

    public FavoriteController(DatabaseContext _context)
    {
      this.context = _context;
    }

    // Get/return single record from Favorite table matching on Id given in favoriteId
    // This should not exist, because it allows users without authorization to meddle with other users' favorites, or requires implementation of authorization groups.
    [HttpGet("Favorite/{favoriteId}")]
    public async Task<ActionResult> GetFavorite(int favoriteId)
    {
      var prevFavorite = await this.context.Favorites.FirstOrDefaultAsync(f => f.Id == favoriteId);
      return Ok(prevFavorite);
    }

    // Get/return all users' favorites 
    // To Do: requires an additional authorization check, because only admin users should have this access. 
    // Hence requires user groups with auth levels.
    // 
    // [Route("api/UserFavorite")]
    // [HttpGet]
    [HttpGet("allusers/favorites")]
    public async Task<ActionResult> GetAllUsersFavorites()
    {
      var userName = User.Identity.Name;
      var user = await this.context.Users.FirstOrDefaultAsync(f => f.Username == userName);

      if (user == null)
      {
        return BadRequest(new { error = $"User {user} not found" });
      }
      else
      {
        // var userFavorites = await this.context.UserFavorites.Include(f => f.Favorite).Where(uf => uf.UserId == user.Id).ToListAsync();
        var userFavorites = await this.context.Users.Include(f => f.UserFavorite).ThenInclude(uf => uf.Favorite).ToListAsync();
        return Ok(userFavorites);
      }
    }

    // Get/return a single user's favorites given their (user) id (not user name) in the User table
    // Double checks if id matches with user name
    // [Route("api/UserFavorite")]
    // [HttpGet("{id}")]
    [HttpGet("user/favorite")]
    public async Task<ActionResult> GetUserFavorites()
    {
      var userName = User.Identity.Name;
      var user = await this.context.Users.Include(i => i.UserFavorite).ThenInclude(uf => uf.Favorite).FirstOrDefaultAsync(f => f.Username == userName);
      if (user == null)
      {
        return BadRequest(new { error = $"User {user} not found" });
      }
      else
      {
        // var userFavorite = await this.context.UserFavorites.Include(f => f.Favorite).FirstOrDefaultAsync(u => u.Id == user.Id); // .Include(u => u.Users)
        return Ok(user);
      }
    }

    // Add new favorite in UserFavorite table for user given in userId.
    // Checks if favorite already exists in Favorite table, and only creates it if it doesn't already exist there
    // Also double checks if user name given in token in Authorization property in request header matches given user id
    // [Route("api/UserFavorite")]
    // [HttpPost("{id}")]
    [HttpPost("user/favorite")]
    public async Task<ActionResult> CreateUserFavorite(NewUserFavoriteViewModel newUserFavoriteViewModel)
    {
      int FavoriteId = 0;
      var userName = User.Identity.Name;
      var user = await this.context.Users.FirstOrDefaultAsync(f => f.Username == userName);

      if (userName != newUserFavoriteViewModel.Username)
      {
        return BadRequest(new { error = $"Authenticated user name ({userName} differs from payload user name {newUserFavoriteViewModel.Username})" });
      }
      int userId = user.Id;
      if (user.Id != userId)
      {
        return BadRequest(new { error = $"Authenticated user id ({user.Id}) differs from endpoint id ({userId})" });
      }
      else
      if (user == null)
      {
        return BadRequest(new { error = $"User {userName} not found" });
      }

      // Find out if the favorite already exists for the given user, if it does, return error message
      int prevUserFavoritesCount = await this.context.UserFavorites.Include(i => i.Favorite).Where(uf => uf.UserId == userId && uf.Favorite.fdcId == newUserFavoriteViewModel.fdcId).CountAsync();
      if (prevUserFavoritesCount != 0)
      {
        return BadRequest(new { error = $"Favorite with fdcId:{newUserFavoriteViewModel.fdcId} already exists for user {userName}" });
      }

      // Find out if the favorite already exists in the database; if it doesn't, create it
      var prevFavorite = await this.context.Favorites.FirstOrDefaultAsync(f => f.fdcId == newUserFavoriteViewModel.fdcId);
      if (prevFavorite == null)
      {
        // it doesn't, so create a new record in the Favorite table
        var newFavorite = new Favorite
        {
          fdcId = newUserFavoriteViewModel.fdcId
        };
        await this.context.Favorites.AddAsync(newFavorite);
        await this.context.SaveChangesAsync();
        FavoriteId = newFavorite.Id;
      }
      else
      {
        // it does, so retrieve the Id to use for new record in UserFavorite table
        FavoriteId = prevFavorite.Id;
      }
      var newUserFavorite = new UserFavorite();
      newUserFavorite.UserId = user.Id;
      newUserFavorite.FavoriteId = FavoriteId;
      await this.context.UserFavorites.AddAsync(newUserFavorite);
      await this.context.SaveChangesAsync();
      return Ok(newUserFavorite); //.Include(f => f.Favorites));
    }

    // Deletes a favorite from UserFavorite table if given favoriteId matches UserFavorite.FavoriteId
    // Also deletes the favorite record from Favorite table if by matching Favorite.Id == favoriteId if no other user has it favorited
    [HttpDelete("user/favorite/{favoriteId}")]
    public async Task<ActionResult> DeleteUserFavorite(int favoriteId)
    {
      // get the user name from the header Authorization data item
      var userName = User.Identity.Name;
      // get the user record from the Users table
      var user = await this.context.Users.FirstOrDefaultAsync(u => u.Username == userName);

      if (user == null)
      {
        return BadRequest(new { error = $"User not found" });
      }
      // get the record with matching user id and favorite id from the UserFavorite table
      var prevUserFavoriteViewModel = await this.context.UserFavorites.FirstOrDefaultAsync(uf => uf.Favorite.Id == favoriteId && uf.UserId == user.Id);
      if (prevUserFavoriteViewModel == null)
      {
        return BadRequest(new { error = $"No favorite record found with Id:{favoriteId}" });
      }
      else
      {
        // get the record from the Favorite table that may (or may not) get deleted further down
        var prevFavorite = await this.context.Favorites.FirstOrDefaultAsync(f => f.Id == favoriteId);
        // find the number of records in the UserFavorite table that refer to the same favorite
        int prevUserFavoriteCount = await this.context.UserFavorites.Where(w => w.FavoriteId == favoriteId).CountAsync();
        // if there is only one record, remove it
        if (prevUserFavoriteCount == 1)
        {
          this.context.Remove(prevFavorite);
        }
        this.context.Remove(prevUserFavoriteViewModel);
        await this.context.SaveChangesAsync();
        return Ok();
      }
    }
  }
}