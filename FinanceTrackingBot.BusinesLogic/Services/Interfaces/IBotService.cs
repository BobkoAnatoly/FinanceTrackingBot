using Telegram.Bot;

namespace FinanceTrackingBot.BusinesLogic.Services.Interfaces
{
    public interface IBotService
    {
        Task<TelegramBotClient> GetBot();
    }
}
