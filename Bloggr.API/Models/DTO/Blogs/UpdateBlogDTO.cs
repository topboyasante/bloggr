namespace Bloggr.API.Models.DTO.Blogs
{
    public class UpdateBlogDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public IFormFile? Image { get; set; }
    }
}