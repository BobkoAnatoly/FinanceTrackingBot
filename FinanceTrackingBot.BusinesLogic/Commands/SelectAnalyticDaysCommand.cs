using System;
using FinanceTrackingBot.BusinesLogic.Services.Implementations;
using FinanceTrackingBot.BusinesLogic.Services.Interfaces;
using FinanceTrackingBot.Common.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceTrackingBot.BusinesLogic.Commands
{
	public class SelectAnalyticDaysCommand : BaseCommand
    {
        private readonly TelegramBotClient _telegramBotClient;
        private readonly IUserService _userService;

        public SelectAnalyticDaysCommand(BotService bot, IUserService userService)
        {
            _telegramBotClient = bot.GetBot().Result;
            _userService = userService;
        }

        public override string Name => CommandNames.SelectAnalyticDaysCommand;
        public override async Task ExecuteAsync(Update update)
        {
            var user = await _userService.Auth(update);

            //var inlineKeyboard = new InlineKeyboardMarkup(new[]
            //{
            //    //new []
            //    //{
            //    //    new InlineKeyboardButton{Text = "Аналитика за 1", CallbackData = "analytic-1"},
            //    //    new InlineKeyboardButton{Text = "Аналитика за 7", CallbackData = "analytic-7"},
            //    //    new InlineKeyboardButton{Text = "Аналитика за 14", CallbackData = "analytic-14"},
            //    //},
            //    //new []
            //    //{
            //    //    new InlineKeyboardButton{Text = "Аналитика за 30", CallbackData = "analytic-30"},
            //    //    new InlineKeyboardButton{Text = "Аналитика за 90", CallbackData = "analytic-90"},
            //    //    new InlineKeyboardButton{Text = "Аналитика за 365", CallbackData = "analytic-365"},
            //    //}
            //});

            //await _telegramBotClient.SendTextMessageAsync(user.ChatId, "Выберите количество дней за которые нужна аналитика",
            //    ParseMode.Markdown, replyMarkup: inlineKeyboard);
        }
    }
}

