using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.API.Models.Domain;
using NewZealandWalks.API.Models.DTO;
using NewZealandWalks.API.Models.Mapping;
using NewZealandWalks.API.Repositories;

namespace NewZealandWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;

        //Constructeur
        public WalksController(IWalkRepository walkRepository)
        {
            this.walkRepository = walkRepository;
        }

        // CREATE Walk
        // POST: /api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // Map DTO to Domain Model
            Walk walkDomainModel = addWalkRequestDto.ToWalk();

            await walkRepository.CreateAsync(walkDomainModel);

            // Map Domain model to DTO
            WalkDto walkDto = walkDomainModel.ToWalkDto();
            return Ok(walkDto);
        }

        // GET Walks
        // GET: /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            List<Walk> walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy,
                    isAscending ?? true, pageNumber, pageSize);

            // Map Domain Model to DTO
            //return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));

            List<WalkDto> walksDto = walksDomainModel.Select(w => w.ToWalkDto()).ToList();
            return Ok(walksDto);
        }

        // Get Walk By Id
        // GET: /api/Walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain model to DTO
            WalkDto walkDto = walkDomainModel.ToWalkDto();
            return Ok(walkDto);
        }

        // Update Walk By Id
        // PUT: /api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            // Map DTO to Domain Model
            //var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
            var walkDomainModel = updateWalkRequestDto.ToWalk();

            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            //return Ok(mapper.Map<WalkDto>(walkDomainModel));
            // Map Domain model to DTO
            WalkDto walkDto = walkDomainModel.ToWalkDto();
            return Ok(walkDto);
        }

        // Delete a Walk By Id
        // DELETE: /api/Walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await walkRepository.DeleteAsync(id);

            if (deletedWalkDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            //return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));
            WalkDto walkDto = deletedWalkDomainModel.ToWalkDto();
            return Ok(walkDto);
        }
    }
}