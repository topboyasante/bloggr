namespace Bloggr.API.Models.Domain
{
    public class Blog
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? CoverImageURL { get; set; }
        public string? CoverImagePublicId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string UserId { get; set; } = string.Empty;
        public User User { get; set; }
    }
}