
using BlogPostManagement.Dto;
using BlogPostManagement.Models;

namespace BlogPostManagement.Services
{
    public interface IUserService
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task RegisterUserAsync(UserDto userDto);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
}
