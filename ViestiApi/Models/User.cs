using System.ComponentModel.DataAnnotations;

namespace ViestiApi.Models
{
    public class User
    {
        public long Id { get; set; }
        [MaxLength(100)]
        public string? UserName { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Password { get; set; } = string.Empty;
        public byte[]? Salt { get; set; } 
        public string? FirstName { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? LastName { get; set; } = string.Empty;
        [MaxLength(100)]
        public DateTime? JoinDate { get; set; }
        public DateTime? LastLogin { get; set; }
    }

    public class UserDTO
    {
               
        [MaxLength(100)]
        public string? UserName { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? FirstName { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? LastName { get; set; } = string.Empty;
        [MaxLength(100)]
        public DateTime? JoinDate { get; set; }
        public DateTime? LastLogin { get; set; }
    }















}

    

