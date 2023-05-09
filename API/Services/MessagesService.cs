using APITrassBank.Context;
using APITrassBank.Models;
using Microsoft.EntityFrameworkCore;

namespace APITrassBank.Services
{
    public interface IMessagesService
    {
        Task Create(string idSelf, MessageCreateDTO model);
        Task<IEnumerable<MessageDTO>> GetMessages(string id);
    }
    public class MessagesService:IMessagesService
    {
        private readonly IContextDB _contextDB;

        public MessagesService(IContextDB contextDB)
        {
            _contextDB = contextDB;
        }

        public Task Create(string idSelf, MessageCreateDTO model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MessageDTO>> GetMessages(string id)
        {
            var messages=await _contextDB.Messages.Where(m=>m.User.Id == id).Select(m =>new MessageDTO
            {
                Body = m.Body,
                Date=m.Date,
                IsReaded = m.IsReaded,
                ReciverName=m.Reciver.UserName,
                RevicerId=m.Reciver.Id,
                Title=m.Title
            }).ToListAsync();
            return messages;
        }

    }
}
