
using BlogPostManagement.Models;

namespace BlogPostManagement.Services
{
    public interface ITokenService
    {
         string GenerateJwtToken(User user);
       
    }
}
