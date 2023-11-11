using Coven.Api.Services;
using Coven.Data.Entities;
using Coven.Data.Repository;
using Coven.Logic.Request_Models.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coven.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IWorldAnvilService _worldAnvilService;
        private readonly IRepository _repository;

        public UserController(IWorldAnvilService worldAnvilService, IRepository repository)
        {
            _worldAnvilService = worldAnvilService;
            _repository = repository;
        }
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult> GetAllUsers()
        {
            return Ok((await _repository.GetDTOUsers()));
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserRequestModel model)
        {
            return Ok(await _repository.CreateUser(model.username, model.worldAnvilUsername, model.email));
        }
    }
}
