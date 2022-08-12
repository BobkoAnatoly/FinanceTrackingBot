using FinanceTrackingBot.BusinesLogic.Services.Implementations;
using FinanceTrackingBot.Common.Enums;
using Telegram.Bot;

namespace FinanceTrackingBot.BusinesLogic.Commands
{
	public class AddOperationCommand : BaseCommand
    {
        private readonly TelegramBotClient _botClient;

        public AddOperationCommand(BotService bot)
        {
            _botClient = bot.GetBot().Result;
        }

        public override string Name => CommandNames.AddOperationCommand;

        public override async Task ExecuteAsync(Telegram.Bot.Types.Update update)
        {
            const string message = "Для добавления новой операции укажите сумму и описание операции в формате: \n" +
                                   "Доход/Расход - \"+/-100:Анатолий вернул/взял в долг\"";

            await _botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, message);
        }
    }
}

