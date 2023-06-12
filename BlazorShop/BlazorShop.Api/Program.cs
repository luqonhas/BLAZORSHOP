using BlazorShop.Domain.Handlers.Authentications;
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
using Microsoft.IdentityModel.Tokens;
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

// Adding Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlazorShop.Api", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n Enter 'Bearer'[space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Connecting DB
builder.Services.AddDbContext<BlazorShopContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication(options =>
{
    // Authentications
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Validations
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("blazorShop-authentication-key")),
        ClockSkew = TimeSpan.FromMinutes(30),
        ValidIssuer = "blazorShop",
        ValidAudience = "blazorShop"
    };
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
builder.Services.AddTransient<ListCartHandle, ListCartHandle>();
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



#region Authentications
// Commands:
builder.Services.AddTransient<LoginEmailHandle, LoginEmailHandle>();
builder.Services.AddTransient<LoginUserNameHandle, LoginUserNameHandle>();
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
