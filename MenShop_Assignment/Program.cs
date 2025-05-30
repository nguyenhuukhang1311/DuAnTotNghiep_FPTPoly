﻿
using MenShop_Assignment.Datas;

using MenShop_Assignment.Mapper.MapperCategory;
using MenShop_Assignment.Mapper.MapperProduct;
using MenShop_Assignment.Repositories.Category;
using MenShop_Assignment.Repositories.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

using MenShop_Assignment.Mapper;
using MenShop_Assignment.Repositories;


using Microsoft.OpenApi.Models;
using MenShop_Assignment.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<OutputReceiptViewRepository>();
builder.Services.AddScoped<OutputReceiptMapper>();
builder.Services.AddScoped<BranchProductRepository>();
builder.Services.AddScoped<BranchMapper>();
builder.Services.AddScoped<InputReceiptRepository>();
builder.Services.AddScoped<ReceiptMapper>();
builder.Services.AddScoped<SizeRepository>();
builder.Services.AddScoped<SizeMapper>();
builder.Services.AddScoped<ColorRepository>();
builder.Services.AddScoped<ColorMapper>();
builder.Services.AddScoped<FabricRepository>();
builder.Services.AddScoped<FabricMapper>();
builder.Services.AddScoped<UserMapper>();
builder.Services.AddScoped<IOrderCustomerRepository, OrderCustomerRepository>();
builder.Services.AddScoped<IStorageRepository, StorageRepository>();
builder.Services.AddScoped<IAutoOrderService, AutoOrderService>();
builder.Services.AddScoped<IOrderCustomerRepository, OrderCustomerRepository>();
builder.Services.AddScoped<OrderMapper>();

// Add services to the container.
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();

builder.Services.AddScoped<ICategoryProductRepository, CategoryProductRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
//mapper
builder.Services.AddScoped<CategoryProductMapper>();
builder.Services.AddScoped<ProductMapper>();


// Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    options.CustomSchemaIds(type => type.FullName);
});



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<ProductMapper>();


    builder.Services.AddScoped<StorageDetailMapper>();

    builder.Services.AddScoped<IStorageRepository, StorageRepository>();

    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();


    var app = builder.Build();

    // Cấu hình Swagger middleware
    if (app.Environment.IsDevelopment())
    {

        app.UseSwagger();
        app.UseSwaggerUI();
    }
    //cho phép xem file tĩnh
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
        RequestPath = "/StaticFiles"
    });

    app.UseRouting();
    app.UseCors("AllowAll");
    app.UseAuthentication();
    app.UseAuthorization();

    //if (app.Environment.IsDevelopment())
    //{
    //    app.MapOpenApi();
    //    app.UseSwagger();
    //    app.UseSwaggerUI();
    //}
    app.UseHttpsRedirection();



    app.MapControllers();
    app.Run();
