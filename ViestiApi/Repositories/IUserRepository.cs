using ViestiApi.Models;

namespace ViestiApi.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserAsync(long id);
        Task<User?> GetUserAsync(string username);
        Task<User?> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(long id);
    }
}