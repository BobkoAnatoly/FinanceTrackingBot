using FinanceTrackingBot.Model.Models;

namespace FinanceTrackingBot.BusinesLogic.Services.Interfaces
{
	public interface IOperationService
	{
		Task<Operation> GetLast(long userId);
		Task<List<Operation>> GetOperations(long userId, DateTime byDate);
	}
}

