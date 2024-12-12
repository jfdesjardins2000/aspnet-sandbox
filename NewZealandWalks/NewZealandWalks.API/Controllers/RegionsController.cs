using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.API.Models.Domain;

namespace NewZealandWalks.API.Controllers
{
    // https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {


        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public IActionResult GetAll()
        {
            // Get Data From Database - Domain models
            var regions = new List<Region>()
            {
                new Region(){ Id = Guid.NewGuid(), Name="Auckland Region", Code="AKL", RegionImageUrl="https://auckland..." },
                new Region(){ Id = Guid.NewGuid(), Name="Wellington Region", Code="WLG", RegionImageUrl="https://wellington..." },
                new Region(){ Id = Guid.NewGuid(), Name="Jasper Region", Code="JSP", RegionImageUrl="https://jasper..." },
            };

            return Ok(regions);

        }
    }
}
