using ViestiApi.Models;

namespace ViestiApi.Repositories
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetMessagesAsync();
        Task<Message?> GetMessageAsync(long id);
        Task<IEnumerable<Message>> GetMyReceivedMessages(User user);
        Task<Message?> CreateMessageAsync(Message message);
        Task<bool> UpdateMessageAsync(Message message);
        Task<bool> DeleteMessageAsync(long id);
        
    }
}
