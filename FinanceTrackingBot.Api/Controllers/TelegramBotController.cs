using FinanceTrackingBot.BusinesLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace FinanceTrackingBot.Api.Controllers
{
    [ApiController]
    [Route("api/message/update")]
    public class TelegramBotController : ControllerBase
    {
        private readonly ICommandExecutorService _commandExecutor;

        public TelegramBotController(ICommandExecutorService commandExecutor)
        {
            _commandExecutor = commandExecutor;
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] object update)
        {
            // /start => register user

            var upd = JsonConvert.DeserializeObject<Update>(update.ToString());

            if (upd?.Message?.Chat == null && upd?.CallbackQuery == null)
            {
                return Ok();
            }

            try
            {
                await _commandExecutor.Execute(upd);
            }
            catch (Exception e)
            {
                return Ok();
            }

            return Ok();
        }
    }
}
