using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using Tardigrade.Logic.Request_Models.Get;

namespace Tardigrade.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        private readonly OpenAIAPI _openAIAPI;
        private readonly IConfiguration _config;

        public AIController(IConfiguration config)
        {
            _config = config;
            _openAIAPI = new OpenAIAPI(_config["OPENAI_API_KEY"]);
        }

        [HttpPost("ChatTest")]
        public async Task<ActionResult> ChatTest([FromBody]GetAIResponseTestModel model)
        {
            var chat = _openAIAPI.Chat.CreateConversation();
            chat.AppendUserInput(model.userInput);

            string response = await chat.GetResponseFromChatbot();


            return Ok(new
            {
                userInput = model.userInput,
                message = response
            });
        }
    }
}
