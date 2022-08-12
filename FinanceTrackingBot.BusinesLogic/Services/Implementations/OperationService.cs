using System;
using FinanceTrackingBot.BusinesLogic.Services.Interfaces;
using FinanceTrackingBot.Model;
using FinanceTrackingBot.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackingBot.BusinesLogic.Services.Implementations
{
	public class OperationService: IOperationService
	{
        private readonly ApplicationDbContext _context;

        public OperationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Operation> GetLast(long userId)
        {
            return await _context.Operations.OrderBy(x => x.CreatedAt).LastOrDefaultAsync(x => x.UserId == userId && !x.IsFinished);
        }

        public async Task<List<Operation>> GetOperations(long userId, DateTime byDate)
        {
            return await _context.Operations.OrderBy(x => x.CreatedAt).Where(x => x.CreatedAt >= byDate).ToListAsync();
        }
    }
}

