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
        Task ReadMessage(string idSelf, string id);
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
        /// <summary>
        /// Creates a new message
        /// </summary>
        /// <param name="idSelf">Id of app user creator</param>
        /// <param name="model">DTO with info for new message</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If sender or reciver are not found</exception>
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
        /// <summary>
        /// Get all messages related to app user id
        /// </summary>
        /// <param name="id">Id of app user</param>
        /// <returns>List of messages</returns>
        public async Task<IEnumerable<MessageDTO>> GetMessages(string id)
        {
            var messages = await _contextDB.Proyecto_Messages.Include(m => m.User).Where(m => m.Reciver.Id == id).Select(m => new MessageDTO
            {
                Body = m.Body,
                Date = m.Date,
                IsReaded = m.IsReaded,
                SenderName = m.User.UserName,
                SenderId = m.User.Id,
                Title = m.Title,
                Id = m.Id.ToString()
            }).ToListAsync();
            return messages;
        }
        /// <summary>
        /// Change message read status to 1
        /// </summary>
        /// <param name="idSelf">Id of reciver</param>
        /// <param name="id">Id of message</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">If message is not found</exception>
        public async Task ReadMessage(string idSelf, string id)
        {
            var message = await _contextDB.Proyecto_Messages
                .Where(m => m.Id.ToString() == id && m.Reciver.Id == idSelf)
                .FirstOrDefaultAsync() ?? throw new ArgumentOutOfRangeException();
            message.IsReaded = true;
            await _contextDB.SaveChangesAsync();
        }
    }
}
