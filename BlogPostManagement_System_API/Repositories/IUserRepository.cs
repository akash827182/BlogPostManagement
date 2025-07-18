using BlogPostManagement.Entities;
using BlogPostManagement.Models;

namespace BlogPostManagement.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string Username);
        Task AddAsync(User user);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
