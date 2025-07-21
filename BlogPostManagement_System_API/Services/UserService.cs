
using BlogPostManagement.Dto;
using BlogPostManagement.Models;
using BlogPostManagement.Repositories;

namespace BlogPostManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterUserAsync(UserDto userDto)
        {
            if (userDto == null || string.IsNullOrEmpty(userDto.Username) || string.IsNullOrEmpty(userDto.Password))
            {
                throw new ArgumentException("Username and password are required");
            }

            var existingUser = await _userRepository.GetByUsernameAsync(userDto.Username);
            if (existingUser != null)
            {
                throw new Exception("Username already exists");
            }

            var user = new User
            {
                Username = userDto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
            };
            await _userRepository.AddAsync(user);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username cannot be null or empty");
            }

            var user = await _userRepository.GetByUsernameAsync(username);
            return user;
        }

        

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(static u => new UserDto
            {
                Username = u.Username,
                Password = "hidden"

            });
        }
    }

}
