using ViestiApi.Middleware;
using ViestiApi.Models;
using ViestiApi.Repositories;

namespace ViestiApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IUserAuthenticationService _userAuthenticationService;

        public UserService(IUserRepository repository, IUserAuthenticationService authService)
        {
            _repository = repository;
            _userAuthenticationService = authService;
        }

        public async Task<UserDTO?> CreateUserAsync(User user)
        {
            User? oldUser = await _repository.GetUserAsync(user.UserName);
            if (oldUser != null)
            {
                return null;
            }
            user.JoinDate = DateTime.Now;

            user = _userAuthenticationService.CreateUserCredentials(user);

            return userToDto(await _repository.CreateUserAsync(user));
        }

        public async Task<bool> DeleteUserAsync(string username)
        {
            var user = await _repository.GetUserAsync(username);
            if (user == null)
            {
                return false;
            }

            return await _repository.DeleteUserAsync(user.Id);
        }

        public async Task<UserDTO?> GetUserAsync(long id)
        {
            var user = await _repository.GetUserAsync(id);
            if (user == null)
            {
                return null;
            }

            return userToDto(user);
        }
        public async Task<UserDTO?> GetUserAsync(string username)
        {
            var user = await _repository.GetUserAsync(username);
            if (user == null)
            {
                return null;
            }

            return userToDto(user);
        }
        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var users = await _repository.GetUsersAsync();
            List<UserDTO> result = new List<UserDTO>();
            foreach (User usr in users)
            {
                var dto = userToDto(usr);
                if (dto != null) result.Add(dto);
            }

            return result;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            if (user == null)
            {
                return false;
            }

            return await _repository.UpdateUserAsync(user);
        }

       

        private UserDTO? userToDto(User? user)
        {
            if(user == null)
            {
                return null;
            }

            UserDTO dto = new UserDTO();
            dto.UserName = user.UserName;
            dto.FirstName = user.FirstName;
            dto.LastName = user.LastName;
            dto.JoinDate = user.JoinDate;
            dto.LastLogin = user.LastLogin;


            return dto;
        }



    }
}