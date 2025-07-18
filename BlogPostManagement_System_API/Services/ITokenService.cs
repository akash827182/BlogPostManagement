using BlogPostManagement.Entities;

namespace BlogPostManagement.Services
{
    public interface ITokenService
    {
         string GenerateJwtToken(User user);
    }
}
