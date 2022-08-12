using Telegram.Bot.Types;

namespace FinanceTrackingBot.BusinesLogic.Services.Interfaces
{
    public interface ICommandExecutorService
    {
        Task Execute(Update update);

    }
}
