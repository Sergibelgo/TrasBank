using APITrassBank.Context;
using APITrassBank.Models;
using Entitys.Entity;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace APITrassBank.Services
{
    public interface IMessagesService
    {
        Task Create(string idSelf, MessageCreateDTO model);
        Task<IEnumerable<MessageDTO>> GetMessages(string id);
    }
    public class MessagesService : IMessagesService
    {
        private readonly ContextDB _contextDB;
        private readonly IUserService _userService;

        public MessagesService(ContextDB contextDB, IUserService userService)
        {
            _contextDB = contextDB;
            _userService = userService;
        }

        public async Task Create(string idSelf, MessageCreateDTO model)
        {
            var user = await _userService.GetUser(idSelf) ?? throw new ArgumentException("User loged not valid");
            var reciver = await _userService.GetUserByUserName(model.ReciverUserName) ?? throw new ArgumentException("Reciver username not valid");

            var messaje = new Message()
            {
                Body = model.Body,
                Title = model.Title,
                Date = DateTime.Now,
            };
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                _contextDB.Users.AttachRange(user, reciver);
                var result = (await _contextDB.Proyecto_Messages.AddAsync(messaje)).Entity;
                result.User = user;
                result.Reciver = reciver;
                await _contextDB.SaveChangesAsync(true);
                scope.Complete();
            }
            catch
            {
                scope.Dispose();
                throw;
            }

        }

        public async Task<IEnumerable<MessageDTO>> GetMessages(string id)
        {
            var messages = await _contextDB.Proyecto_Messages.Include(m => m.User).Where(m => m.Reciver.Id == id).Select(m => new MessageDTO
            {
                Body = m.Body,
                Date = m.Date,
                IsReaded = m.IsReaded,
                SenderName = m.User.UserName,
                SenderId = m.User.Id,
                Title = m.Title
            }).ToListAsync();
            return messages;
        }

    }
}
