using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.API.Models.Domain;
using NewZealandWalks.API.Models.DTO;
using NewZealandWalks.API.Models.Mapping;

namespace NewZealandWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {

        //Constructeur
        public WalksController()
        {
        }

        // CREATE Walk
        // POST: /api/walks
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {

            // Map DTO to Domain Model
            Walk walkDomainModel = addWalkRequestDto.ToWalk();

        }


    }
}
