using AutoMapper;
using Bloggr.API.Models.Domain;
using Bloggr.API.Models.DTO.Blogs;
using Bloggr.API.Repositories.Blogs;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Bloggr.API.Services.Blogs
{
    public class BlogService : IBlogService
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IMapper mapper;
        private readonly Cloudinary cloudinary;
        private readonly IBlogRepository blogRepository;

        public BlogService(IHttpContextAccessor contextAccessor, IMapper mapper, Cloudinary cloudinary, IBlogRepository blogRepository)
        {
            this.contextAccessor = contextAccessor;
            this.mapper = mapper;
            this.cloudinary = cloudinary;
            this.blogRepository = blogRepository;
        }
        public async Task<BlogDTO> Create(CreateBlogDTO createBlogDTO)
        {
            try
            {
                var userId = string.Empty;
                if (contextAccessor.HttpContext != null)
                {
                    userId = contextAccessor.HttpContext.User?.Identity?.Name;
                }

                var BlogDomain = mapper.Map<Blog>(createBlogDTO);

                var uploadResult = new ImageUploadResult();

                if (createBlogDTO.Image != null && createBlogDTO.Image.Length > 0)
                {
                    using var stream = createBlogDTO.Image.OpenReadStream();
                    var uploadParams = new ImageUploadParams
                    {
                        Folder = "bloggr",
                        File = new FileDescription(createBlogDTO.Image.FileName, stream),
                        Transformation = new Transformation().Crop("fill").Gravity("face").Width(500).Height(500)
                    };

                    uploadResult = await cloudinary.UploadAsync(uploadParams);

                    if (uploadResult.Url == null || uploadResult.PublicId == null)
                    {
                        throw new Exception("Image upload failed.");
                    }
                }

                BlogDomain.UserId = userId;
                BlogDomain.CoverImageURL = uploadResult.Url?.ToString();
                BlogDomain.CoverImagePublicId = uploadResult.PublicId;
                BlogDomain.CreatedAt = DateTime.UtcNow;
                BlogDomain.UpdatedAt = DateTime.UtcNow;

                var Blog = await blogRepository.CreateAsync(BlogDomain);

                return mapper.Map<BlogDTO>(Blog);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating the blog", ex);
            }
        }

        public async Task<BlogDTO?> Delete(Guid id)
        {
            try
            {
                var userId = string.Empty;

                if (contextAccessor.HttpContext == null)
                {
                    throw new Exception("User is not authenticated.");
                }

                userId = contextAccessor.HttpContext.User?.Identity?.Name;

                var BlogDomain = await blogRepository.FindOneAsync(id) ?? throw new InvalidOperationException("No blog exists with the provided ID.");

                if (BlogDomain.UserId != userId)
                {
                    throw new Exception("You are not authorized to delete this blog.");
                }

                if (!string.IsNullOrEmpty(BlogDomain.CoverImagePublicId))
                {
                    await cloudinary.DeleteResourcesAsync([BlogDomain.CoverImagePublicId]);
                }

                await blogRepository.DeleteAsync(BlogDomain);

                return mapper.Map<BlogDTO>(BlogDomain);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting the blog.", ex);
            }
        }

        public async Task<List<BlogDTO>> GetAllBlogs()
        {
            var BlogsDomain = await blogRepository.FindAllAsync();
            return mapper.Map<List<BlogDTO>>(BlogsDomain);
        }

        public async Task<BlogDTO> GetBlogById(Guid id)
        {
            var BlogDomain = await blogRepository.FindOneAsync(id);
            return mapper.Map<BlogDTO>(BlogDomain);
        }

        public async Task<BlogDTO?> Update(Guid id, UpdateBlogDTO updateBlogDTO)
        {
            try
            {
                if (contextAccessor.HttpContext == null)
                {
                    throw new Exception("User is not authenticated.");
                }

                var userId = contextAccessor.HttpContext.User?.Identity?.Name;

                var BlogDomain = await blogRepository.FindOneAsync(id) ?? throw new InvalidOperationException("No blog exists with the provided ID.");

                if (BlogDomain.UserId != userId)
                {
                    throw new Exception("You are not authorized to update this blog.");
                }

                if (!string.IsNullOrWhiteSpace(updateBlogDTO.Title))
                {
                    BlogDomain.Title = updateBlogDTO.Title;
                }
                if (!string.IsNullOrWhiteSpace(updateBlogDTO.Description))
                {
                    BlogDomain.Description = updateBlogDTO.Description;
                }
                if (!string.IsNullOrWhiteSpace(updateBlogDTO.Content))
                {
                    BlogDomain.Content = updateBlogDTO.Content;
                }

                if (updateBlogDTO.Image != null && updateBlogDTO.Image.Length > 0)
                {
                    await cloudinary.DeleteResourcesAsync([BlogDomain.CoverImagePublicId]);
                    var uploadResult = new ImageUploadResult();

                    using var stream = updateBlogDTO.Image.OpenReadStream();
                    var uploadParams = new ImageUploadParams
                    {
                        Folder = "bloggr",
                        File = new FileDescription(updateBlogDTO.Image.FileName, stream),
                        Transformation = new Transformation().Crop("fill").Gravity("face").Width(500).Height(500)
                    };

                    uploadResult = await cloudinary.UploadAsync(uploadParams);
                    BlogDomain.CoverImageURL = uploadResult.Url.ToString();
                    BlogDomain.CoverImagePublicId = uploadResult.PublicId;
                }

                BlogDomain = await blogRepository.UpdateAsync(BlogDomain);

                return mapper.Map<BlogDTO>(BlogDomain);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the blog.", ex);
            }
        }
    }
}