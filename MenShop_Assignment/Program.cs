
using MenShop_Assignment.Datas;
using MenShop_Assignment.Mapper;
using MenShop_Assignment.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
//Khu vưc của bảo ---------------------------------------------------------------
builder.Services.AddScoped<OutputReceiptViewRepository>();
builder.Services.AddScoped<OutputReceiptMapper>();
builder.Services.AddScoped<BranchProductRepository>();
builder.Services.AddScoped<BranchMapper>();
builder.Services.AddScoped<InputReceiptRepository>();
builder.Services.AddScoped<InputReceiptMapper>();
//Khu vưc của bảo ---------------------------------------------------------------
// Add services to the container.
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
//
builder.Services.AddScoped<ProductMapper>();
builder.Services.AddScoped<CustomerAddressMapper>();
builder.Services.AddScoped<StorageDetailMapper>();
builder.Services.AddScoped<OrdersMapper>();
builder.Services.AddScoped<GHTKOrderMapper>();
builder.Services.AddScoped<GHTKProductMapper>();
builder.Services.AddScoped<IStorageRepository, StorageRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IGHTKOrderRepository, GHTKOrderRepository>();
//
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "MenShop_Assignment", Version = "v1" });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
