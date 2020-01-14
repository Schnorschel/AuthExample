using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AuthExample.Controllers
{
  [ApiController]
  [Route("fdc/[controller]")]

  public class FoodController : ControllerBase
  {
    [HttpGet]
    public async Task<ActionResult> Search(string searchTerm, string pageNumber)
    {
      var url = $"https://api.nal.usda.gov/fdc/v1/search?api_key=gDM0ZXrJf7KbErIMxVX0a23yfl64EaWNmiV176kd&generalSearchInput={searchTerm}&pageNumber={pageNumber}"; // sample url
      using (var client = new HttpClient())
      {
        Console.WriteLine(url);
        var data = await client.GetStringAsync(url);
        return Content(data, "application/json");
      }
    }

    [HttpGet("{fdcId}")]
    public async Task<ActionResult> Detail(int fdcId)
    {
      var url = $"https://api.nal.usda.gov/fdc/v1/{fdcId}?api_key=gDM0ZXrJf7KbErIMxVX0a23yfl64EaWNmiV176kd"; // sample url
      using (var client = new HttpClient())
      {
        Console.WriteLine(url);
        var data = await client.GetStringAsync(url);
        return Content(data, "application/json");
      }
    }
  }
}