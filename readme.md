# Bloggr - A Backend API for Blogs

### Stack:
- C#
- ASP.NET
- PostgreSQL
- Docker
- Cloudinary


### Models
- User : IdentityUser
```json
{
    // Additional Fields
    "first_name": string,
    "last_name": string,
}
```
- Blog :
```json
{
    // Additional Fields
    "title": string,
    "description": string,
    "cover_img_url": string,
    "cover_img_public_id": string,
    "content": string,
    "created_at": Date,
    "updated_at": Date,
}
```

### Features:
- Everyone can:
  - [x]  Sign In
  - [x]  Sign Up
  - [x]  Read Blogs
- Authenticated users can:
  - [x]  Create Blogs
  - [x]  Update Blogs
  - [x]  Delete Blogs