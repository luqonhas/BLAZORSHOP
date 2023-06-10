using BlazorShop.Domain.Handlers.Carts;
using BlazorShop.Domain.Handlers.Categories;
using BlazorShop.Domain.Handlers.ItemsCart;
using BlazorShop.Domain.Handlers.Products;
using BlazorShop.Domain.Handlers.Users;
using BlazorShop.Domain.Interfaces;
using BlazorShop.Infra.Data.Contexts;
using BlazorShop.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });

// Adding CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlazorShop.Api", Version = "v1" });
});

// Connecting DB
builder.Services.AddDbContext<BlazorShopContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
    options.HttpsPort = 5001;
});



// Independency Injections:

#region Users
builder.Services.AddTransient<IUserRepository, UserRepository>();

// Commands:
builder.Services.AddTransient<CreateUserHandle, CreateUserHandle>();
builder.Services.AddTransient<DeleteUserHandle, DeleteUserHandle>();

// Queries:
builder.Services.AddTransient<ListUserHandle, ListUserHandle>();
builder.Services.AddTransient<SearchUserByIdHandle, SearchUserByIdHandle>();
builder.Services.AddTransient<SearchUserByEmailHandle, SearchUserByEmailHandle>();
#endregion



#region Categories
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

// Commands:
builder.Services.AddTransient<CreateCategoryHandle, CreateCategoryHandle>();
builder.Services.AddTransient<DeleteCategoryHandle, DeleteCategoryHandle>();

// Queries:
builder.Services.AddTransient<ListCategoryHandle, ListCategoryHandle>();
builder.Services.AddTransient<SearchCategoryByIdHandle, SearchCategoryByIdHandle>();
#endregion



#region Products
builder.Services.AddTransient<IProductRepository, ProductRepository>();

// Commands:
builder.Services.AddTransient<CreateProductHandle, CreateProductHandle>();
builder.Services.AddTransient<DeleteProductHandle, DeleteProductHandle>();

// Queries:
builder.Services.AddTransient<ListProductHandle, ListProductHandle>();
builder.Services.AddTransient<SearchProductByIdHandle, SearchProductByIdHandle>();
builder.Services.AddTransient<SearchProductByNameHandle, SearchProductByNameHandle>();
#endregion



#region Carts
builder.Services.AddTransient<ICartRepository, CartRepository>();

// Commands:
builder.Services.AddTransient<CreateCartHandle, CreateCartHandle>();
builder.Services.AddTransient<DeleteCartHandle, DeleteCartHandle>();

// Queries:
builder.Services.AddTransient<SearchCartByIdHandle, SearchCartByIdHandle>();
builder.Services.AddTransient<SearchCartByUserIdHandle, SearchCartByUserIdHandle>();
#endregion



#region ItemsCart
builder.Services.AddTransient<IItemCartRepository, ItemCartRepository>();

// Commands:
builder.Services.AddTransient<CreateItemCartHandle, CreateItemCartHandle>();
builder.Services.AddTransient<DeleteItemCartHandle, DeleteItemCartHandle>();

// Queries:
builder.Services.AddTransient<ListItemCartHandle, ListItemCartHandle>();
builder.Services.AddTransient<SearchItemCartByIdHandle, SearchItemCartByIdHandle>();
#endregion



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
