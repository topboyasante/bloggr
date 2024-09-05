using Bloggr.API.Models.Domain;

namespace Bloggr.API.Repositories.Blogs
{
    public interface IBlogRepository
    {
        Task<List<Blog>> FindAllAsync();
        Task<Blog?> FindOneAsync(Guid id);
        Task<Blog> CreateAsync(Blog blog);
        Task<Blog> UpdateAsync(Blog blog);
        Task DeleteAsync(Blog blog);
    }
}