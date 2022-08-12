using System;
using FinanceTrackingBot.BusinesLogic.Services.Implementations;
using FinanceTrackingBot.BusinesLogic.Services.Interfaces;
using FinanceTrackingBot.Common.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FinanceTrackingBot.BusinesLogic.Commands
{
	public class GetAnalyticsCommand : BaseCommand
    {
        private readonly IUserService _userService;
        private readonly IAnalyticService _analyticService;
        private readonly TelegramBotClient _telegramBotClient;

        public GetAnalyticsCommand(IUserService userService, IAnalyticService analyticService, BotService bot)
        {
            _userService = userService;
            _analyticService = analyticService;
            _telegramBotClient = bot.GetBot().Result;
        }

        public override string Name => CommandNames.GetAnalyticsCommand;
        public override async Task ExecuteAsync(Update update)
        {
            var user = await _userService.Auth(update);
            var daysString = update.CallbackQuery?.Data?.Replace("analytic-", "") ?? "0";
            var days = int.Parse(daysString);
            var analyticData = await _analyticService.GetAnalytic(update, days);

            await _telegramBotClient.SendTextMessageAsync(user.ChatId, analyticData, ParseMode.Markdown);
        }
    }
}

