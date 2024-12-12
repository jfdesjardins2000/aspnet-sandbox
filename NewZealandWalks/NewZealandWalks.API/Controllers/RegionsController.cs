using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.API.Data;
using NewZealandWalks.API.Models.Domain;

namespace NewZealandWalks.API.Controllers
{
    // https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public IActionResult GetAll()
        {
            //// Get Data From Database - Domain models
            //var regions = new List<Region>()
            //{
            //    new Region(){ Id = Guid.NewGuid(), Name="Auckland Region", Code="AKL", RegionImageUrl="https://auckland..." },
            //    new Region(){ Id = Guid.NewGuid(), Name="Wellington Region", Code="WLG", RegionImageUrl="https://wellington..." },
            //    new Region(){ Id = Guid.NewGuid(), Name="Jasper Region", Code="JSP", RegionImageUrl="https://jasper..." },
            //};

            var regions = _dbContext.Region;

            return Ok(regions);

        }


        // GET SINGLE REGION (Get Region By ID)
        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            // Get Region Domain Model From Database
            var regionDomain = await _dbContext.Region.FindAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Return DTO back to client
            return Ok(regionDomain);
        }
    }
}
