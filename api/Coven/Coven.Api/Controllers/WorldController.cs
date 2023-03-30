using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Coven.Api.Services;

namespace Tardigrade.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorldController : ControllerBase
    {
        private readonly IWorldAnvilService WorldAnvilService;

        public WorldController(IWorldAnvilService _worldAnvilService)
        {
            WorldAnvilService = _worldAnvilService;
        }

        [HttpGet("GetWorldInfo")]
        public async Task<ActionResult> GetWorldInfo() 
        {
            return Ok(await WorldAnvilService.GetWorld(new Guid()));
        }
    }
}
