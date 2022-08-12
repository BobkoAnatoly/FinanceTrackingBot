using FinanceTrackingBot.Api.Services.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceTrackingBot.Api.Services.Implementations
{
	public class ButtonsService:IButtonsService
	{
        public ReplyKeyboardMarkup MenuButton(KeyboardButton[] buttonsFirstRow,
            KeyboardButton[]? buttonsSecondRow = default,
            KeyboardButton[]? buttonsThirdRow = default)
        {
            ReplyKeyboardMarkup keyboard;
            if (buttonsThirdRow != default)
            {
                keyboard = new(new[]
                    { buttonsFirstRow,
                        buttonsSecondRow!,
                        buttonsThirdRow!});
            }
            else if (buttonsSecondRow != default)
            {
                keyboard = new(new[]
                    { buttonsFirstRow,
                        buttonsSecondRow!});
            }
            else
                keyboard = new(buttonsFirstRow);
            keyboard.ResizeKeyboard = true;
            return keyboard;
        }
    }
}

