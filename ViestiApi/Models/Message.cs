using System.ComponentModel.DataAnnotations;

namespace ViestiApi.Models
{
    public class Message // Tietokantaan tallennettava viesti
    {
        public long Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public string Body { get; set; }
        public User Sender { get; set; }
        public User? Receiver { get; set; }
        public Message? PrevMessage {get; set;}
        public DateTime? SendTime { get; set; }
        
    }
    public class MessageDTO // Verkon yli lähetettävä viesti
    {
        public long Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public string Body { get; set; }
        public string SenderID { get; set; }
        public string? ReceiverID{ get; set; }
        public long? PrevMessageID { get; set; }
        public DateTime? SendTime { get; set; }

    }
}