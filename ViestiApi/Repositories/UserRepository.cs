using Microsoft.EntityFrameworkCore;
using ViestiApi.Models;

namespace ViestiApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MessageContext _context;

        public UserRepository(MessageContext context)
        {
            _context = context;
        }

        public async Task<User?> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User?> GetUserAsync(long id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetUserAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var dbUser = await _context.Users.FindAsync(user.Id);
            if (dbUser == null)
            {
                return false;
            }

            //dbUser.UserName = user.UserName;
            dbUser.Password = user.Password;
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            //dbUser.JoinDate = user.JoinDate;
            //dbUser.LastLogin = user.LastLogin;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                // TODO: add logging / error handling as needed
                return false;
            }

            return true;
        }
    }
}