
using BlogPostManagement.Dto;
using BlogPostManagement.Models;
using BlogPostManagement.Repositories;

namespace BlogPostManagement.Services
{
    public class BlogPostService(IBlogPostRepository blogPostRepository) : IBlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository = blogPostRepository;

        public async Task<BlogPostDto> GetBlogPostByIdAsync(int id)
        {
            var blogPost = await _blogPostRepository.GetBlogPostByIdAsync(id);
            if (blogPost == null)
            {
                throw new KeyNotFoundException("Blog post not found.");
            }
            return new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Author = blogPost.Author,
                Content = blogPost.Content,
                CreatedAt = blogPost.CreatedAt,
                UpdatedAt = blogPost.UpdatedAt,
                IsPublished= blogPost.IsPublished
            };

        }

        public async Task<IEnumerable<BlogPostDto>> GetAllBlogPostsAsync()
        {
            var blogPosts = await _blogPostRepository.GetAllBlogPostsAsync();
            return blogPosts.Select(bp => new BlogPostDto   // instead we can use a separate private method to map entities to DTOs
            {
                Id = bp.Id,
                Title = bp.Title,
                Author = bp.Author,
                Content = bp.Content,
                CreatedAt = bp.CreatedAt,
                UpdatedAt = bp.UpdatedAt
            });
        }

        public async Task<BlogPostDto> CreateBlogPostAsync(BlogPostDto blogPostDto)
        {

            if (string.IsNullOrEmpty(blogPostDto.Title) || string.IsNullOrEmpty(blogPostDto.Content))
            {
                throw new ArgumentException("Title and content are required");
            }

            var blogPost = new BlogPost
            {
                Title = blogPostDto.Title,
                Author = blogPostDto.Author,
                Content = blogPostDto.Content,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await _blogPostRepository.AddAsync(blogPost);
            blogPostDto.Id = blogPost.Id; // Id alloted after adding to the repository
            return blogPostDto;
        }

        public async Task UpdateBlogPostAsync(int id, BlogPostDto blogPostDto)
        {
            var previousBlogPost = await _blogPostRepository.GetBlogPostByIdAsync(id);
            if (previousBlogPost == null)
            {
                throw new KeyNotFoundException("Blog post not found.");
            }
            previousBlogPost.Title = blogPostDto.Title;
            previousBlogPost.Author = blogPostDto.Author;
            previousBlogPost.Content = blogPostDto.Content;
            previousBlogPost.UpdatedAt = DateTime.UtcNow;
            await _blogPostRepository.UpdateAsync(previousBlogPost);
        }

        public async Task DeleteBlogPostAsync(int id)
        {
            var blogPost = await _blogPostRepository.GetBlogPostByIdAsync(id);
            if (blogPost == null)
            {
                throw new KeyNotFoundException("Blog post not found.");
            }
            await _blogPostRepository.DeleteAsync(id);

        }


    }
}
