using Telegram.Bot.Types;

namespace FinanceTrackingBot.BusinesLogic.Services.Interfaces
{
	public interface IUserService
	{
		Task<Model.Models.User> Auth(Update update);
	}
}

