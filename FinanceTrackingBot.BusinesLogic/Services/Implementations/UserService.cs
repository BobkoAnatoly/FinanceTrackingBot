using FinanceTrackingBot.BusinesLogic.Services.Interfaces;
using FinanceTrackingBot.Model;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FinanceTrackingBot.BusinesLogic.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Model.Models.User> Auth(Update update)
        {
            var newUser = update.Type switch
            {
                UpdateType.CallbackQuery => new Model.Models.User
                {
                    UserName = update.CallbackQuery.From.Username,
                    ChatId = (int)update.CallbackQuery.Message.Chat.Id,
                    FirstName = update.CallbackQuery.Message.From.FirstName,
                    LastName = update.CallbackQuery.Message.From.LastName
                },
                UpdateType.Message => new Model.Models.User
                {
                    UserName = update.Message.Chat.Username,
                    ChatId = (int)update.Message.Chat.Id,
                    FirstName = update.Message.Chat.FirstName,
                    LastName = update.Message.Chat.LastName
                }
            };

            var user = await _context.Users.FirstOrDefaultAsync(x => x.ChatId == newUser.ChatId);

            if (user != null) return user;

            var result = await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return result.Entity;
        }
    }
}

