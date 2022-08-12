using FinanceTrackingBot.BusinesLogic.Services.Implementations;
using FinanceTrackingBot.BusinesLogic.Services.Interfaces;
using FinanceTrackingBot.Common.Enums;
using FinanceTrackingBot.Model;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FinanceTrackingBot.BusinesLogic.Commands
{
	public class FinishOperationCommand : BaseCommand
    {
        private readonly TelegramBotClient _botClient;
        private readonly IUserService _userService;
        private readonly IOperationService _operationService;
        private readonly ApplicationDbContext _context;

        public FinishOperationCommand(IUserService userService, BotService bot, IOperationService operationService, ApplicationDbContext context)
        {
            _userService = userService;
            _botClient = bot.GetBot().Result;
            _operationService = operationService;
            _context = context;
        }

        public override string Name => CommandNames.FinishOperationCommand;

        public override async Task ExecuteAsync(Update update)
        {
            var user = await _userService.Auth(update);
            var operation = await _operationService.GetLast(user.Id);
            operation.IsFinished = true;
            operation.CategoryId = (int?)long.Parse(update.Message.Text);

            await _context.SaveChangesAsync();
            await _botClient.SendTextMessageAsync(user.ChatId, "Операция добавлена!", ParseMode.Markdown);
        }
    }
}

