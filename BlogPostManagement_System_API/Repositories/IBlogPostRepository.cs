using BlogPostManagement.Models;

namespace BlogPostManagement.Repositories
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> GetBlogPostByIdAsync(int id);
        Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync();
        Task AddAsync(BlogPost blogPost);
        Task UpdateAsync(BlogPost blogPost);
        Task DeleteAsync(int id);
    }
}
