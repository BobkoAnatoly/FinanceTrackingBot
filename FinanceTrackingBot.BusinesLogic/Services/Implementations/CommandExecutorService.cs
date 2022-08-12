using FinanceTrackingBot.BusinesLogic.Commands;
using FinanceTrackingBot.BusinesLogic.Services.Interfaces;
using FinanceTrackingBot.Common.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FinanceTrackingBot.BusinesLogic.Services.Implementations
{
    public class CommandExecutorService : ICommandExecutorService
    {
        private readonly List<BaseCommand> _commands;
        private BaseCommand _lastCommand;

        public async Task Execute(Update update)
        {
            if (update?.Message?.Chat == null && update?.CallbackQuery == null)
                return;

            if (update.Type == UpdateType.Message)
            {
                switch (update.Message?.Text)
                {
                    case "Создать операцию":
                        await ExecuteCommand(CommandNames.AddOperationCommand, update);
                        return;
                    case "Получить операции":
                        await ExecuteCommand(CommandNames.GetOperationsCommand, update);
                        return;
                    case "Аналитика":
                        await ExecuteCommand(CommandNames.SelectAnalyticDaysCommand, update);
                        return;
                }
            }

            if (update.Type == UpdateType.CallbackQuery)
            {
                if (update.CallbackQuery.Data.Contains("analytic"))
                {
                    await ExecuteCommand(CommandNames.GetAnalyticsCommand, update);
                    return;
                }
            }

            if (update.Message != null && update.Message.Text.Contains(CommandNames.StartCommand))
            {
                await ExecuteCommand(CommandNames.StartCommand, update);
                return;
            }

            // AddOperation => SelectCategory => FinishOperation
            switch (_lastCommand?.Name)
            {
                case CommandNames.AddOperationCommand:
                    {
                        await ExecuteCommand(CommandNames.SelectCategoryCommand, update);
                        break;
                    }
                case CommandNames.SelectCategoryCommand:
                    {
                        await ExecuteCommand(CommandNames.FinishOperationCommand, update);
                        break;
                    }
                case null:
                    {
                        await ExecuteCommand(CommandNames.StartCommand, update);
                        break;
                    }
            }
        }
        private async Task ExecuteCommand(string commandName, Update update)
        {
            _lastCommand = _commands.First(x => x.Name == commandName);
            await _lastCommand.ExecuteAsync(update);
        }
    }
}
