
using AutoMapper;
using BlogPostManagement.Dto;
using BlogPostManagement.Models;
using BlogPostManagement.Repositories;

namespace BlogPostManagement.Services
{
    public class BlogPostService(IBlogPostRepository blogPostRepository,IMapper mapper) : IBlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository = blogPostRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BlogPostDto> GetBlogPostByIdAsync(int id)
        {
            var blogPost = await _blogPostRepository.GetBlogPostByIdAsync(id);
            if (blogPost == null)
            {
                throw new KeyNotFoundException("Blog post not found.");
            }
            //return new BlogPostDto
            //{
            //    Id = blogPost.Id,
            //    Title = blogPost.Title,
            //    Author = blogPost.Author,
            //    Content = blogPost.Content,
            //    CreatedAt = blogPost.CreatedAt,
            //    UpdatedAt = blogPost.UpdatedAt,
            //    IsPublished= blogPost.IsPublished
            //};
            return _mapper.Map<BlogPostDto>(blogPost);

        }

        public async Task<IEnumerable<BlogPostDto>> GetAllBlogPostsAsync()
        {
            var blogPosts = await _blogPostRepository.GetAllBlogPostsAsync();
            return _mapper.Map<IEnumerable<BlogPostDto>>(blogPosts);
        }

        public async Task<BlogPostDto> CreateBlogPostAsync(BlogPostDto blogPostDto)
        {
            if (string.IsNullOrEmpty(blogPostDto.Title) || string.IsNullOrEmpty(blogPostDto.Content))
            {
                throw new ArgumentException("Title and content are required");
            }

            var blogPost = _mapper.Map<BlogPost>(blogPostDto);
            blogPost.CreatedAt = DateTime.UtcNow;
            blogPost.UpdatedAt = DateTime.UtcNow;
            await _blogPostRepository.AddAsync(blogPost);
            return _mapper.Map<BlogPostDto>(blogPost);
        }

        public async Task UpdateBlogPostAsync(int id, BlogPostDto blogPostDto)
        {
            var previousBlogPost = await _blogPostRepository.GetBlogPostByIdAsync(id);
            if (previousBlogPost == null)
            {
                throw new KeyNotFoundException("Blog post not found.");
            }

            _mapper.Map(blogPostDto, previousBlogPost);
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
