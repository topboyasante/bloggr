using System.Text;
using Bloggr.API.Data;
using Bloggr.API.Models.Domain;
using Bloggr.API.Profiles;
using Bloggr.API.Repositories.Auth;
using Bloggr.API.Repositories.Blogs;
using Bloggr.API.Repositories.Email;
using Bloggr.API.Services.Blogs;
using CloudinaryDotNet;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

Cloudinary cloudinary = new(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
cloudinary.Api.Secure = true;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Bloggr API",
        Version = "v1",
        Description = "This is a RESTful API built with .NET and PostgreSQL that allows users to write blogs for others to read.",
        Contact = new OpenApiContact
        {
            Name = "Nana Kwasi Asante",
            Email = "asantekwasi101@gmail.com",
            Url = new Uri("https://www.github.com/topboyasante")
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
        },
    });
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme{
            Reference = new OpenApiReference{
                Type=ReferenceType.SecurityScheme,
                Id=JwtBearerDefaults.AuthenticationScheme
            },
            Scheme="Oauth2",
            Name=JwtBearerDefaults.AuthenticationScheme,
            In=ParameterLocation.Header
        },
        new List<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
    ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY") ?? "my_jwt_key"))
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING")));

builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

builder.Services.AddScoped<IBlogService, BlogService>();

builder.Services.AddTransient<IEmailSender<User>, EmailRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddSingleton(cloudinary);

builder.Services.AddIdentityCore<User>()
.AddRoles<IdentityRole>()
.AddTokenProvider<DataProtectorTokenProvider<User>>("Bloggr")
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
