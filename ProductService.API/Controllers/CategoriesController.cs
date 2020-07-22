namespace ProductService.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using MicroservicesPOC.Shared.API.Controllers;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            await Task.Delay(50);
            return this.Ok(new string[] { "house", "vehicle", "family", "travel", "work", "pet" });
        }
    }
}
