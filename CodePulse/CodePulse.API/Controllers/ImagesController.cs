using CodePulse.API.Repositories.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {

            var response = "les images!!!";

            return Ok(response);
        }
    }
}
