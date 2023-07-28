using AutoMapper;
using GeekWorld.Application.Contracts.Services;
using GeekWorld.Application.Implementations;
using GeekWorld.Domain.Contracts;
using GeekWorld.Infrastructure.Database.Configs;
using GeekWorld.Application.Mappers;
using GeekWorld.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GeekWorld.WebAPI.Security;
using Microsoft.OpenApi.Models;
using MyAuthorize.WebAPI.Security;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


services.AddAutoMapper(typeof(Program));

var mapperConfiguration = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapperConfig());
});

var mapper = mapperConfiguration.CreateMapper();
services.AddSingleton(mapper);

services.AddControllers();
services.AddEndpointsApiExplorer();

services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Geek World - API", Version = "snapshot 0.0.1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IPostRepository, PostRepository>();
services.AddScoped<IFriendshipRepository, FriendshipRepository>();
services.AddScoped<ICommentRepository, CommentRepository>();

services.AddScoped<IUserService, UserServiceImp>();
services.AddScoped<IAuthService, AuthServiceImp>();
services.AddScoped<IMeService, MeServiceImp>();
services.AddScoped<IPostService, PostServiceImp>();
services.AddScoped<IFriendshipService, FriendshipServiceImp>();
services.AddScoped<ICommentService, CommentServiceImp>();

services.AddScoped<TokenService>();

services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});


services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenSettings.SecretKey)),
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalhost3000");


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
