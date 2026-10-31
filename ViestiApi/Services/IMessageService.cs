using ViestiApi.Models;

namespace ViestiApi.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageDTO>> GetMessagesAsync();
        Task<MessageDTO?> GetMessageAsync(long id);
        Task<IEnumerable<MessageDTO>> GetMyReceivedMessages(string username);
        Task<MessageDTO?> CreateMessageAsync(MessageDTO message);
        Task<bool> UpdateMessageAsync(MessageDTO message);
        Task<bool> DeleteMessageAsync(long id);

    }
}
