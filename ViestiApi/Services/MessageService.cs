using Microsoft.AspNetCore.Http.HttpResults;
using ViestiApi.Models;
using ViestiApi.Repositories;

namespace ViestiApi.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repository;
        private readonly IUserRepository _userRepository;

        public MessageService(IMessageRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;

        }
        public async Task<MessageDTO?> CreateMessageAsync(MessageDTO message)
        {
            message.SendTime = DateTime.Now;
            return MessageToDTO(await _repository.CreateMessageAsync(await DTOToMessage(message)));
        }

        public async Task<bool> DeleteMessageAsync(long id)
        {
            Message? message = await _repository.GetMessageAsync(id);
            if(message == null)
            {
                return false;
            }
            return await _repository.DeleteMessageAsync(id);
        }

        public async Task<MessageDTO?> GetMessageAsync(long id)
        {
          
            return MessageToDTO(await _repository.GetMessageAsync(id));
        }

        public async Task<IEnumerable<MessageDTO>> GetMessagesAsync()
        {
            IEnumerable<Message> messages = await _repository.GetMessagesAsync();
            List<MessageDTO> result = new List<MessageDTO>();
            foreach(Message msg in messages)
            {
                result.Add(MessageToDTO(msg));
            }

            return result;
        }

        public async Task<bool> UpdateMessageAsync(MessageDTO message)
        {
            Message? dbMessage = await _repository.GetMessageAsync(message.Id);
            if (dbMessage == null)
            {
                return false;
            }
          
            return await _repository.UpdateMessageAsync(await DTOToMessage(message));
        }


        private MessageDTO? MessageToDTO(Message message)
        {
            MessageDTO dto = new MessageDTO();

            if(message == null)
            {
                return null;
            }

            dto.Id = message.Id;
            dto.Title = message.Title;
            dto.Body = message.Body;
            dto.SenderID = message.Sender.UserName;
            dto.SendTime = message.SendTime;
            if (message.Receiver !=null)
            {
                dto.ReceiverID = message.Receiver.UserName;
            }
            if(message.PrevMessage != null)
            {
                dto.PrevMessageID = message.PrevMessage.Id;
            }

            return dto;
        }

        private async Task<Message>DTOToMessage(MessageDTO dto)
        {
            Message msg = new Message();
            msg.Id = dto.Id;
            msg.Title = dto.Title;
            msg.Body = dto.Body;
            msg.SendTime = dto.SendTime;

            User? sender = await _userRepository.GetUserAsync(dto.SenderID);
            if(sender != null)
            {
                msg.Sender = sender;
            }
           if(dto.ReceiverID != null)
            {
                User? receiver = await _userRepository.GetUserAsync(dto.ReceiverID);
                if(receiver != null)
                {
                    msg.Receiver = receiver;
                }
            }
           if(dto.PrevMessageID != null && dto.PrevMessageID != 0)
            {
                Message prevMessage = await _repository.GetMessageAsync((long)dto.PrevMessageID);
            }
            return msg;
        }

        public async Task<IEnumerable<MessageDTO>> GetMyReceivedMessages(string username)
        {
            User? user = await _userRepository.GetUserAsync(username);
            if (user == null)
            {
                return null;
            }
            IEnumerable<Message> messages = await _repository.GetMyReceivedMessages(user);
            List<MessageDTO> result = new List<MessageDTO>();
            foreach (Message msg in messages)
            {
                result.Add(MessageToDTO(msg));
            }
            return result;
        }
    }
}
