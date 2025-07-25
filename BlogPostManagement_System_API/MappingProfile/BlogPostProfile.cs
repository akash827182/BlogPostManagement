using AutoMapper;
using BlogPostManagement.Dto;
using BlogPostManagement.Models;

namespace BlogPostManagement.MappingProfile
{
    public class BlogPostProfile:Profile
    {
        public BlogPostProfile()
        {
            CreateMap<BlogPost, BlogPostDto>().ReverseMap();
        }
    }
}
