using Coven.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coven.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PineconeController : ControllerBase
    {
        private readonly IPineconeService PineconeService;

        public PineconeController(IPineconeService _pinecone)
        {
            PineconeService = _pinecone;
        }

        [HttpGet("GetIndexes")]
        public async Task<ActionResult> GetIndexes()
        {
            return Ok(await PineconeService.QueryPineconeIndex());
        }
    }
}
