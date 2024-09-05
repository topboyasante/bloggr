namespace Bloggr.API.Models.DTO.Blogs
{
    public class CreateBlogDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
    }
}