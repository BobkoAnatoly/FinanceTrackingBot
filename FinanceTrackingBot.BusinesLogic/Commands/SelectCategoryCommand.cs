using System;
using System.Text;
using FinanceTrackingBot.BusinesLogic.Services.Implementations;
using FinanceTrackingBot.BusinesLogic.Services.Interfaces;
using FinanceTrackingBot.Common.Enums;
using FinanceTrackingBot.Model;
using FinanceTrackingBot.Model.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FinanceTrackingBot.BusinesLogic.Commands
{
	public class SelectCategoryCommand : BaseCommand
    {
        private readonly TelegramBotClient _botClient;
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public SelectCategoryCommand(BotService bot, ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
            _botClient = bot.GetBot().Result;
        }

        public override string Name => CommandNames.SelectCategoryCommand;

        public override async Task ExecuteAsync(Update update)
        {
            var priceAndName = update.Message.Text.Split(':');
            var user = await _userService.Auth(update);

            var operation = new Operation
            {
                Name = priceAndName[1],
            };

            if (priceAndName[0].IndexOf('-') != -1)
            {
                operation.Price = decimal.Parse(priceAndName[0].Remove(priceAndName[0].IndexOf('-'), 1));
                operation.Type = OperationType.Discharge;
            }
            else
            {
                operation.Price = decimal.Parse(priceAndName[0]);
                operation.Type = OperationType.Income;
            }

            await _context.Operations.AddAsync(operation);
            await _context.SaveChangesAsync();

            var categories = _context.Categories.Where(x => x.UserId == user.Id && x.Type == operation.Type).ToList();
            var message = new StringBuilder("Операция добавлена, осталось выбрать категорию! \n" +
                                            $"Тип операции:{operation.Type.ToString()}\n" +
                                            $"Сумма:{operation.Price}\n");

            foreach (var category in categories)
            {
                message.AppendLine($"{category.Id} : {category.Name}");
            }

            message.AppendLine("Выберите категорию отправив сообщение с номером категории");

            await _botClient.SendTextMessageAsync(update.Message.Chat.Id, message.ToString(), ParseMode.Markdown);
        }
    }
}

