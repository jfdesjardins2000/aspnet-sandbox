using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // POST To Create New Region
        // POST: https://localhost:portnumber/api/regions
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map or Convert DTO to Domain Model
            Region regionDomainModel = addRegionRequestDto.ToRegion();

            // Use Domain Model to create Region
            _dbContext.Region.Add(regionDomainModel);
            _dbContext.SaveChanges();
            //regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            // Map Domain model back to DTO
            RegionDto regionDto = regionDomainModel.ToRegionDto();

            //Retourne http code 201 et le Id
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var existingRegion = _dbContext.Region.FirstOrDefault(x => x.Id == id);

            if (existingRegion == null)
            {
                return NotFound();
            }

            existingRegion.Code = updateRegionRequestDto.Code;
            existingRegion.Name = updateRegionRequestDto.Name;
            existingRegion.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            _dbContext.SaveChanges();

            return Ok(existingRegion);
        }

        // Delete Region
        // DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Region? regionDomainModel = _dbContext.Region.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            _dbContext.Region.Remove(regionDomainModel);
            _dbContext.SaveChanges(true);

            RegionDto regionDto = regionDomainModel.ToRegionDto();

            return Ok(regionDto);
        }
    }
}