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
    public async Task<ActionResult> Search(string searchTerm, string pageNumber = "1", string includeDataTypeList = null, string requireAllWords = null)
    {
      searchTerm = Uri.EscapeUriString(searchTerm);
      var url = $"https://api.nal.usda.gov/fdc/v1/search?api_key=gDM0ZXrJf7KbErIMxVX0a23yfl64EaWNmiV176kd&generalSearchInput={searchTerm}&pageNumber={pageNumber}"; // sample url
      if (includeDataTypeList != null)
      {
        url += "&includeDataTypeList=" + Uri.EscapeUriString(includeDataTypeList);
      }
      if (requireAllWords != null)
      {
        url += "&requireAllWords=true";
      }
      // if (sortOrder != null)
      // {
      //   //add the page number to the url
      //   url += $"sortBy={sortOrder}";
      // }
      using (var client = new HttpClient())
      {
        Console.WriteLine(url);
        // var data = await client.GetStringAsync(url);

        // return Content(data, "application/json");
        var response = await client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
          string responseBody = await response.Content.ReadAsStringAsync();
          return Content(responseBody, "application/json");
        }
        else
        {
          return BadRequest(new { error = $@"Origin server sent status '{response.StatusCode}'" });
        }
      }
    }

    [HttpGet("{fdcId}")]
    public async Task<ActionResult> Detail(int fdcId)
    {
      var url = $"https://api.nal.usda.gov/fdc/v1/{fdcId}?api_key=gDM0ZXrJf7KbErIMxVX0a23yfl64EaWNmiV176kd"; // sample url
      using (var client = new HttpClient())
      {
        Console.WriteLine(url);
        // var data = await client.GetStringAsync(url);
        // return Content(data, "application/json");
        var response = await client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
          string responseBody = await response.Content.ReadAsStringAsync();
          return Content(responseBody, "application/json");
        }
        else
        {
          return BadRequest(new { error = $@"Origin server sent status '{response.StatusCode}'" });
        }

      }
    }
  }
}