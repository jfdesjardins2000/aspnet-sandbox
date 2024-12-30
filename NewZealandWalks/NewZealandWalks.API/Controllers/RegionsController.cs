using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewZealandWalks.API.CustomActionFilters;
using NewZealandWalks.API.Data;
using NewZealandWalks.API.Models.Domain;
using NewZealandWalks.API.Models.DTO;
using NewZealandWalks.API.Models.Mapping;
using NewZealandWalks.API.Repositories;
using Serilog;

namespace NewZealandWalks.API.Controllers
{
    // https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        //private readonly ILogger<RegionsController> _logger;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            _dbContext = dbContext;
            _regionRepository = regionRepository;
        }

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetAll()
        {
            Log.Information("GetAll regions request received.");

            var regionsDomain = await _regionRepository.GetAllAsync();

            var regionDtoList = regionsDomain.Select(r => r.ToRegionDto());

            Log.Information("GetAll regions request processed successfully.");

            return Ok(regionDtoList);
        }

        // GET SINGLE REGION (Get Region By ID)
        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            Log.Information("GetById request received for region ID: {Id}", id);

            var regionDomain = await _regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                Log.Warning("Region with ID: {Id} not found.", id);
                return NotFound();
            }

            var regionDto = regionDomain.ToRegionDto();

            Log.Information("GetById request processed successfully for region ID: {Id}", id);

            return Ok(regionDto);
        }

        // POST To Create New Region
        // POST: https://localhost:portnumber/api/regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            Log.Information("Create region request received.");

            var regionDomainModel = await _regionRepository.CreateAsync(addRegionRequestDto.ToRegion());

            var regionDto = regionDomainModel.ToRegionDto();

            Log.Information("Region created successfully with ID: {Id}", regionDto.Id);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [ValidateModel]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            Log.Information("Update region request received for ID: {Id}", id);

            var updatedRegion = await _regionRepository.UpdateAsync(id, updateRegionRequestDto.ToRegion());

            if (updatedRegion == null)
            {
                Log.Warning("Region with ID: {Id} not found for update.", id);
                return NotFound();
            }

            Log.Information("Region with ID: {Id} updated successfully.", id);

            return Ok(updatedRegion);
        }

        // Delete Region
        // DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Log.Information("Delete region request received for ID: {Id}", id);

            var regionDto = await _regionRepository.DeleteAsync(id);

            if (regionDto == null)
            {
                Log.Warning("Region with ID: {Id} not found for deletion.", id);
                return NotFound();
            }

            Log.Information("Region with ID: {Id} deleted successfully.", id);

            return Ok(regionDto);
        }
    }
}