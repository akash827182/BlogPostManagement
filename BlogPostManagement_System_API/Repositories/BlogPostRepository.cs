using BlogPostManagement.Data;
using BlogPostManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogPostManagement.Repositories
{
    public class BlogPostRepository:IBlogPostRepository
    {
        private readonly BlogDbContext _blogdbcontext;
        public BlogPostRepository(BlogDbContext context)
        {
            _blogdbcontext = context;
        }

        public async Task<BlogPost> GetBlogPostByIdAsync(int id)
        {
            return await _blogdbcontext.BlogPosts.FindAsync(id) ?? throw new KeyNotFoundException(" Blog post not found.");
        }
        public async Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync()
        {
            return await _blogdbcontext.BlogPosts.ToListAsync();
        }
        public async Task AddAsync(BlogPost blogPost)
        {
            await _blogdbcontext.BlogPosts.AddAsync(blogPost);
            await _blogdbcontext.SaveChangesAsync();
        }
        public async Task UpdateAsync(BlogPost blogPost)
        {
            _blogdbcontext.BlogPosts.Update(blogPost);
            await _blogdbcontext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var blogPost = await GetBlogPostByIdAsync(id);
            _blogdbcontext.BlogPosts.Remove(blogPost);
            await _blogdbcontext.SaveChangesAsync();
        }

    }
}
