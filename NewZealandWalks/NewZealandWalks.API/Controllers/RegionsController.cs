using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.API.Data;
using NewZealandWalks.API.Models.Domain;
using NewZealandWalks.API.Models.DTO;
using NewZealandWalks.API.Models.Mapping;

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
            //return Ok(regions);


            var regionsDomain = _dbContext.Region.ToList();

            //TODO : ON NE DOIT JAMAIS RETOURNER DIRECTEMENT une classe du DOMAIN MODEL (Region)
            // ON DOI TOUJOURS RETOURNER UN DTO
            //var regionDto = new List<RegionDto>();
            //foreach (var r in regionsDomain)
            //{
            //    regionDto.Add(new RegionDto() { Id = r.Id, Name = r.Name, Code = r.Code, RegionImageUrl = r.RegionImageUrl });
            //}
            //// Return DTOs
            //return Ok(regionDto);

            var regionDtoList = regionsDomain.Select(r => r.ToRegionDto());

            // Return DTOs
            return Ok(regionDtoList);
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
            //return Ok(regionDomain);

            //Map/Covert Region Domain Model to Region DTO
            //RegionDto regionDto = new RegionDto() { Id = regionDomain.Id, Name = regionDomain.Name, Code = regionDomain.Code, RegionImageUrl = regionDomain.RegionImageUrl };
            RegionDto regionDto = regionDomain.ToRegionDto();
            return Ok(regionDto);

        }
    }
}
