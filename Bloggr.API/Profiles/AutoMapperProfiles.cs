using AutoMapper;
using Bloggr.API.Models.Domain;
using Bloggr.API.Models.DTO.Blogs;

namespace Bloggr.API.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CreateBlogDTO, Blog>().ReverseMap();
            CreateMap<UpdateBlogDTO, Blog>().ReverseMap();
            CreateMap<BlogDTO, Blog>().ReverseMap();
        }
    }
}