using System;
using System.Text;
using FinanceTrackingBot.BusinesLogic.Services.Implementations;
using FinanceTrackingBot.BusinesLogic.Services.Interfaces;
using FinanceTrackingBot.Common.Enums;
using FinanceTrackingBot.Model;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FinanceTrackingBot.BusinesLogic.Commands
{
	public class GetOperationsCommand:BaseCommand
	{
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly TelegramBotClient _botClient;

        public GetOperationsCommand(ApplicationDbContext context, IUserService userService, BotService bot)
        {
            _context = context;
            _userService = userService;
            _botClient = bot.GetBot().Result;
        }

        public override string Name => CommandNames.GetOperationsCommand;
        public override async Task ExecuteAsync(Update update)
        {
            var user = await _userService.Auth(update);

            var operations = _context.Operations.Include(x => x.Category)
                .Where(x => x.IsFinished && x.UserId == user.Id).ToList();
            var debits = operations.Where(x => x.Type == OperationType.Discharge).ToList();
            var credits = operations.Where(x => x.Type == OperationType.Income).ToList();

            var message = new StringBuilder("Ваши операции: \n" +
                                            "Доходы: \n");

            foreach (var operation in credits)
            {
                message.AppendLine($"{operation.Name} : {operation.Price} : {operation.CreatedAt}");
            }

            message.AppendLine("Расходы:");

            foreach (var operation in debits)
            {
                message.AppendLine($"{operation.Name} : {operation.Price} : {operation.CreatedAt}");
            }

            await _botClient.SendTextMessageAsync(user.ChatId, message.ToString(), ParseMode.Markdown);
        }
    }
}

