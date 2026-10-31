using ViestiApi.Models;

namespace ViestiApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUsersAsync();
        Task<UserDTO?> GetUserAsync(long id);
        Task<UserDTO?> GetUserAsync(string username);
        Task<UserDTO?> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(string username);
    }
}