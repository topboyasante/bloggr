using Bloggr.API.Models.API;
using Bloggr.API.Models.DTO.Blogs;
using Bloggr.API.Services.Blogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bloggr.API.Controllers
{
    [ApiController]
    [Route("api/blogs")]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService blogService;

        public BlogsController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = new APIResponse();
            var blogs = await blogService.GetAllBlogs();

            if (blogs == null)
            {
                res.Data = null;
                res.Error = "Unable to get blogs";
                return BadRequest(res);
            }

            return Ok(blogs);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetOne([FromRoute] Guid id)
        {
            var res = new APIResponse();
            var blog = await blogService.GetBlogById(id);

            if (blog == null)
            {
                res.Data = null;
                res.Error = "Unable to get blog";
                return BadRequest(res);
            }

            return Ok(blog);
        }


        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Create([FromForm] CreateBlogDTO createBlogDTO)
        {
            var res = new APIResponse();
            var blog = await blogService.Create(createBlogDTO);

            if (blog == null)
            {
                res.Data = null;
                res.Error = "Unable to create blog";
                return BadRequest(res);
            }

            return CreatedAtAction(nameof(GetOne), new { id = blog.Id }, blog);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdateBlogDTO updateBlogDTO)
        {
            var res = new APIResponse();
            var blog = await blogService.Update(id, updateBlogDTO);

            if (blog == null)
            {
                res.Data = null;
                res.Error = "Unable to update blog";
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var res = new APIResponse();
            var blog = await blogService.Delete(id);

            if (blog == null)
            {
                res.Data = null;
                res.Error = "Unable to delete blog";
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}