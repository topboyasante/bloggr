using Bloggr.API.Models.DTO.Blogs;

namespace Bloggr.API.Services.Blogs
{
    public interface IBlogService
    {
        Task<List<BlogDTO>> GetAllBlogs();
        Task<BlogDTO> GetBlogById(Guid id);
        Task<BlogDTO> Create(CreateBlogDTO createBlogDTO);
        Task<BlogDTO?> Update(Guid id, UpdateBlogDTO updateBlogDTO);
        Task<BlogDTO?> Delete(Guid id);
    }
}