using Microsoft.AspNetCore.Mvc;
using Coven.Data.Repository;
using Coven.Logic.Request_Models.Post;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Coven.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IRepository repository;
        public ClientController(IRepository _repository)
        {
            repository = _repository ?? throw new ArgumentNullException(nameof(_repository));
        }

        [HttpGet("GetClient")]
        public async Task<ActionResult> GetClients()
        {
            return Ok(await repository.GetClients());
        }

        // GET api/<ClientController>/5
        [HttpGet("GetClient/{userId}")]
        public async Task<ActionResult> GetClient(Guid userId)
        {
            return Ok(await repository.GetClient(userId));
        }

        [HttpPost("CreateClient")]
        public async Task<ActionResult> CreateClient([FromBody] CreateClientRequestModel model)
        {
            return Ok(await repository.CreateClient(model.username, model.email));
        }
    }
}
