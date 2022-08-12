
using FinanceTrackingBot.BusinesLogic.Services.Implementations;
using FinanceTrackingBot.BusinesLogic.Services.Interfaces;
using FinanceTrackingBot.Common.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceTrackingBot.BusinesLogic.Commands
{
	public class StartCommand : BaseCommand
    {
        private readonly IUserService _userService;
        private readonly TelegramBotClient _botClient;

        public StartCommand(IUserService userService, BotService bot)
        {
            _userService = userService;
            _botClient = bot.GetBot().Result;
        }

        public override string Name => CommandNames.StartCommand;

        public override async Task ExecuteAsync(Update update)
        {
            var user = await _userService.Auth(update);
            //var inlineKeyboard = new ReplyKeyboardMarkup(new[]
            //{
            //    new[]
            //    {
            //        new KeyboardButton
            //        {
            //            Text = "Создать операцию"
            //        },
            //        new KeyboardButton
            //        {
            //            Text = "Получить операции"
            //        },
            //        new KeyboardButton
            //        {
            //            Text = "Аналитика"
            //        }
            //    }
            //});

            //await _botClient.SendTextMessageAsync(user.ChatId, "Добро пожаловать! Я буду вести учёт ваших доходов и расходов! ",
            //    ParseMode.Markdown, replyMarkup: inlineKeyboard);
        }
    }
}

