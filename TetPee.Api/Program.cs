using Microsoft.EntityFrameworkCore;
using TetPee.Api.Extensions;
using TetPee.Api.Middlewares;
using TetPee.Repository;
using TetPee.service.CloudinaryService;
using TetPee.service.MediaService;
using ProductService = TetPee.service.Product;
using JwtService = TetPee.service.JwtService;
using IdentityService = TetPee.service.Identity;
using SellerService = TetPee.service.Seller;
using CategoryService = TetPee.service.Categogy;
using UserService = TetPee.service.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddJwtServices(builder.Configuration);
builder.Services.AddSwaggerServices();

builder.Services.AddScoped<IService, Service>();
builder.Services.AddScoped<ProductService.IService, ProductService.Service>();
builder.Services.AddScoped<IdentityService.IService, IdentityService.Service>();
builder.Services.AddScoped<SellerService.IService, SellerService.Service>();
builder.Services.AddScoped<CategoryService.IService, CategoryService.Service>();
builder.Services.AddScoped<JwtService.IService, JwtService.Service>();
builder.Services.AddScoped<UserService.IService, UserService.Service>();

builder.Services.AddTransient<GlobalExceptionHandlerMiddlewares>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddlewares>();

// Configure the HTTP request pipeline.0
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();