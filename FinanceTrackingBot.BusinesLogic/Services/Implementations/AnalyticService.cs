using System;
using System.Text;
using FinanceTrackingBot.BusinesLogic.Services.Interfaces;
using FinanceTrackingBot.Common.Enums;
using Telegram.Bot.Types;

namespace FinanceTrackingBot.BusinesLogic.Services.Implementations
{
	public class AnalyticService: IAnalyticService
	{
        private readonly IUserService _userService;
        private readonly IOperationService _operationService;

        public AnalyticService(IUserService userService, IOperationService operationService)
        {
            _userService = userService;
            _operationService = operationService;
        }

        public async Task<string> GetAnalytic(Update update, int days)
        {
            var users = await _userService.Auth(update);
            var operations = await _operationService.GetOperations(users.Id, DateTime.UtcNow.AddDays(-days));

            var debits = operations.Where(x => x.IsFinished && x.Type == OperationType.Discharge).ToList();
            var credits = operations.Where(x => x.IsFinished && x.Type == OperationType.Discharge).ToList();
            var total = credits.Sum(x => x.Price) - debits.Sum(x => x.Price);
            var message = new StringBuilder($"Ваши операции за последние {days} дн.: \n" + "Доходы: \n");

            foreach (var operation in credits)
            {
                message.AppendLine($"{operation.Name} : {operation.Price} : {operation.CreatedAt}");
            }

            message.AppendLine("Расходы:");

            foreach (var operation in debits)
            {
                message.AppendLine($"{operation.Name} : {operation.Price} : {operation.CreatedAt}");
            }

            message.AppendLine($"Итоговая сумма: {total} рублей");

            return message.ToString();
        }
    }
}

