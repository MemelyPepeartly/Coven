using Coven.Api.Services;
using Coven.Data.Entities;
using Coven.Data.Repository;
using Coven.Logic.DTO.AI;
using Coven.Logic.Request_Models.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coven.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IWorldAnvilService WorldAnvilService;
        private readonly IRepository Repository;

        public UserController(IWorldAnvilService _worldAnvilService, IRepository _repository)
        {
            WorldAnvilService = _worldAnvilService;
            Repository = _repository;
        }
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult> GetAllUsers()
        {
            return Ok((await Repository.GetUsers())
                .Select(u => new UserDTO()
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    WorldAnvilUsername = u.WorldAnvilUsername,
                    Email = u.Email,
                    WacharacterSets = u.WacharacterSets.Select(cs => new WACharacterSetDTO()
                    {
                        CharacterSet = cs.CharacterSet,
                        vectors = ByteArrayToFloatArray(cs.Waembeddings.SelectMany(v => v.Vector).ToArray())
                    }).ToList()
                })
                .ToList());
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody]CreateUserRequestModel model)
        {
            return Ok(await Repository.CreateUser(model.username, model.worldAnvilUsername, model.email));
        }

        private static float[] ByteArrayToFloatArray(byte[] byteArray)
        {
            float[] floatArray = new float[byteArray.Length / sizeof(float)];
            Buffer.BlockCopy(byteArray, 0, floatArray, 0, byteArray.Length);
            return floatArray;
        }
    }
}
