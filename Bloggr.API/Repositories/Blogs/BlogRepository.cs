using Bloggr.API.Data;
using Bloggr.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggr.API.Repositories.Blogs
{
    public class BlogRepository(AppDbContext db) : IBlogRepository
    {
        private readonly AppDbContext db = db;

        public async Task<Blog> CreateAsync(Blog blog)
        {
            var res = await db.AddAsync(blog);
            await db.SaveChangesAsync();

            return res.Entity;
        }

        public async Task DeleteAsync(Blog blog)
        {
            db.Remove(blog);
            await db.SaveChangesAsync();
        }

        public Task<List<Blog>> FindAllAsync()
        {
            return db.Blogs.ToListAsync();
        }

        public async Task<Blog?> FindOneAsync(Guid id)
        {
            var res = await db.Blogs.FirstOrDefaultAsync(x => x.Id == id);
            return res;
        }

        public async Task<Blog> UpdateAsync(Blog blog)
        {
            var res = db.Blogs.Update(blog);
            await db.SaveChangesAsync();

            return res.Entity;
        }
    }
}