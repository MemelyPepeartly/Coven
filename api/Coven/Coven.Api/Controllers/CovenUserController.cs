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
    public class CovenUserController : ControllerBase
    {
        private readonly IWorldAnvilService WorldAnvilService;
        private readonly IRepository Repository;

        public CovenUserController(IWorldAnvilService _worldAnvilService, IRepository _repository)
        {
            WorldAnvilService = _worldAnvilService;
            Repository = _repository;
        }
#warning to be deprecated
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult> GetAllUsers()
        {
            return Ok((await Repository.GetDTOUsers()));
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserRequestModel model)
        {
            return Ok(await Repository.CreateUser(model.username, model.worldAnvilUsername, model.email));
        }
    }
}
