using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceTrackingBot.Api.Services.Interfaces
{
	public interface IButtonsService
	{
		ReplyKeyboardMarkup MenuButton(KeyboardButton[] buttonsFirstRow,
			KeyboardButton[]? buttonsSecondRow = default,
			KeyboardButton[]? buttonsThirdRow = default);
	}
}

