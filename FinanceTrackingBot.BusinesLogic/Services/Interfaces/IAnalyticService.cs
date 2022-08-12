using Telegram.Bot.Types;

namespace FinanceTrackingBot.BusinesLogic.Services.Interfaces
{
	public interface IAnalyticService
	{
		Task<string> GetAnalytic(Update update, int days);
	}
}

