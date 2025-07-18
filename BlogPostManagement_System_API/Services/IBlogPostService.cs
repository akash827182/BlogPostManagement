using BlogPostManagement.Models;

namespace BlogPostManagement.Services
{
    public interface IBlogPostService
    {
        Task<BlogPostDto> GetBlogPostByIdAsync(int id);
        Task<IEnumerable<BlogPostDto>> GetAllBlogPostsAsync();
        Task<BlogPostDto> CreateBlogPostAsync(BlogPostDto blogPostDto);
        Task UpdateBlogPostAsync(int id, BlogPostDto blogPostDto);
        Task DeleteBlogPostAsync(int id);
    }
}
