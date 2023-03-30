using Microsoft.AspNetCore.Mvc;
using Tardigrade.Data.Repository;
using Tardigrade.Logic.Request_Models.Post;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tardigrade.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly IRepository repository;
        public CharacterController(IRepository _repository)
        {
            repository = _repository ?? throw new ArgumentNullException(nameof(_repository));
        }

        [HttpGet("GetCharacter")]
        public async Task<ActionResult> GetCharacters()
        {
            return Ok(await repository.GetAllCharacters());
        }

        [HttpGet("GetCharacter/{characterId}")]
        public async Task<ActionResult> GetCharacter(Guid characterId)
        {
            return Ok(await repository.GetCharacter(characterId));
        }

        [HttpPost("CreateCharacter/{userId}")]
        public async Task<ActionResult> CreateCharacter(Guid userId, [FromBody] CreateCharacterRequestModel model)
        {
            return Ok(await repository.CreateCharacter(userId, model.characterName));
        }
    }
}
