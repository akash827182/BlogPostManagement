using System.Text.Json.Serialization;

namespace BlogPostManagement.Dto
{
    public class UserDto
    {
        public required string Username { get; set; }

        //[JsonIgnore]
        public required string Password { get; set; }
        
    }
}
