using MenShop_Assignment.Datas;
using MenShop_Assignment.Mapper;
using MenShop_Assignment.Mapper.MapperCategory;
using MenShop_Assignment.Mapper.MapperOrder;
using MenShop_Assignment.Mapper.MapperProduct;
using MenShop_Assignment.Models.Momo;
using MenShop_Assignment.Repositories;
using MenShop_Assignment.Repositories.AccountRepository;
using MenShop_Assignment.Repositories.AdminRepositories;
using MenShop_Assignment.Repositories.Carts;
using MenShop_Assignment.Repositories.Category;
using MenShop_Assignment.Repositories.CustomerAddress;
using MenShop_Assignment.Repositories.OrderRepositories;
using MenShop_Assignment.Repositories.Product;
using MenShop_Assignment.Services;
using MenShop_Assignment.Services.Momo;
using MenShop_Assignment.Services.PaymentServices;
using MenShop_Assignment.Services.VNPay;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
<<<<<<< HEAD
using MenShop_Assignment.Mapper;
using MenShop_Assignment.Repositories;
using MenShop_Assignment.Models.Momo;
using Microsoft.OpenApi.Models;
using MenShop_Assignment.Repositories.AccountRepository;
using MenShop_Assignment.Repositories.AdminRepositories;
using MenShop_Assignment.Repositories.Carts;
using MenShop_Assignment.Repositories.OrderRepositories;
using MenShop_Assignment.Mapper.MapperOrder;
using MenShop_Assignment.Services.Momo;
using MenShop_Assignment.Services.PaymentServices;
using Microsoft.Extensions.DependencyInjection;
using MenShop_Assignment.Repositories.CustomerAddress;
using MenShop_Assignment.Services.VNPay;
using MenShop_Assignment.Services;
using MenShop_Assignment.Models.Account;
using MenShop_Assignment.Repositories.BranchesRepositories;


=======
using Microsoft.OpenApi.Models;
using OrderMapper = MenShop_Assignment.Mapper.MapperOrder.OrderMapper;
>>>>>>> 88675dc243c071395de510368003d5291069df9e

var builder = WebApplication.CreateBuilder(args);
<<<<<<< HEAD
//Khu vưc của bảo ---------------------------------------------------------------
builder.Services.AddScoped<OutputReceiptViewRepository>();
builder.Services.AddScoped<OutputReceiptMapper>();
builder.Services.AddScoped<BranchProductRepository>();
builder.Services.AddScoped<BranchMapper>();
builder.Services.AddScoped<InputReceiptRepository>();
<<<<<<< HEAD
builder.Services.AddScoped<ReceiptMapper>();
builder.Services.AddScoped<SizeRepository>();
builder.Services.AddScoped<SizeMapper>();
builder.Services.AddScoped<ColorRepository>();
builder.Services.AddScoped<ColorMapper>();
builder.Services.AddScoped<FabricRepository>();
builder.Services.AddScoped<FabricMapper>();
builder.Services.AddScoped<UserMapper>();



=======
builder.Services.AddScoped<InputReceiptMapper>();
//Khu vưc của bảo ---------------------------------------------------------------
>>>>>>> 88675dc243c071395de510368003d5291069df9e
// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddControllers();
<<<<<<< HEAD
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<MapperCustomerAddress>();
builder.Services.AddScoped<IOrderCustomerRepository, OrderCustomerRepository>();
builder.Services.AddScoped<IStorageRepository, StorageRepository>();
builder.Services.AddScoped<IAutoOrderService, AutoOrderService>();
builder.Services.AddScoped<IOrderCustomerRepository, OrderCustomerRepository>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<ICategoryProductRepository, CategoryProductRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<CategoryProductMapper>();
builder.Services.AddScoped<ProductMapper>();
builder.Services.AddScoped<OutputReceiptRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
=======
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
=======

// --------------------------
// Configure Services (DI)
// --------------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Repositories & Mappers
builder.Services.AddScoped<ICategoryProductRepository, CategoryProductRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderCustomerRepository, OrderCustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IStorageRepository, StorageRepository>();
builder.Services.AddScoped<ICustomerAddressRepository, CustomerAddressRepository>();

builder.Services.AddScoped<ProductMapper>();
builder.Services.AddScoped<CategoryProductMapper>();
builder.Services.AddScoped<OrderMapper>();
builder.Services.AddScoped<UserMapper>();
builder.Services.AddScoped<StorageDetailMapper>();
builder.Services.AddScoped<ColorMapper>();
builder.Services.AddScoped<ColorRepository>();
builder.Services.AddScoped<SizeMapper>();
builder.Services.AddScoped<SizeRepository>();
builder.Services.AddScoped<FabricMapper>();
builder.Services.AddScoped<FabricRepository>();
builder.Services.AddScoped<ReceiptMapper>();
builder.Services.AddScoped<InputReceiptRepository>();
builder.Services.AddScoped<BranchMapper>();
builder.Services.AddScoped<BranchProductRepository>();
builder.Services.AddScoped<OutputReceiptMapper>();
builder.Services.AddScoped<OutputReceiptViewRepository>();
builder.Services.AddScoped<MapperCustomerAddress>();

// Services
builder.Services.AddScoped<IAutoOrderService, AutoOrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IVNPayService, VNPayService>();
builder.Services.AddScoped<IMomoServices, MomoServices>();

// Configure Momo API
builder.Services.Configure<MomoOptionModel>(
    builder.Configuration.GetSection("MomoAPI"));
>>>>>>> 4c7a32a113c1670ac083587cc24696b9b1623ec9

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<MomoOptionModel>>().Value);

// --------------------------
// Controllers & JSON Options
// --------------------------
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// --------------------------
// Swagger
// --------------------------
>>>>>>> 88675dc243c071395de510368003d5291069df9e
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MenShop API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập token vào đây theo dạng: Bearer {token}"
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
            Array.Empty<string>()
        }
    });

    c.CustomSchemaIds(type => type.FullName);
});

<<<<<<< HEAD
    builder.Services.AddScoped<StorageDetailMapper>();
    builder.Services.AddScoped<IStorageRepository, StorageRepository>();
    builder.Services.AddScoped<IOrderRepository, OrderRepository>();
    builder.Services.AddScoped<MenShop_Assignment.Mapper.MapperOrder.OrderMapper>();
    builder.Services.AddScoped<MenShop_Assignment.Mapper.OrderMapper>();
    builder.Services.AddScoped<IAccountRepository, AccountRepository>();
    builder.Services.AddScoped<IPaymentService, PaymentService>();
    builder.Services.AddScoped<IAdminRepository, AdminRepository>();
    builder.Services.AddScoped<ICustomerAddressRepository, CustomerAddressRepository>();
    builder.Services.AddScoped<IVNPayService, VNPayService>();


    //connect momoApi
    builder.Services.Configure<MomoOptionModel>(
        builder.Configuration.GetSection("MomoAPI"));
    builder.Services.AddScoped<IMomoServices, MomoServices>();
    builder.Services.AddSingleton(sp =>
        sp.GetRequiredService<IOptions<MomoOptionModel>>().Value);

    // Add Controller
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        });


 

    // CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
    });
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<ProductMapper>();
    builder.Services.AddScoped<StorageDetailMapper>();
    builder.Services.AddScoped<IStorageRepository, StorageRepository>();
    builder.Services.AddScoped<IAccountRepository, AccountRepository>();
    builder.Services.AddControllers()
        .AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            x.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        });


    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
=======
// --------------------------
// CORS
// --------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// --------------------------
// App Middleware Pipeline
// --------------------------
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
>>>>>>> 88675dc243c071395de510368003d5291069df9e
    {
        Console.WriteLine("Seeding admin...");
        await DbInitializer.SeedAdminAsync(services);
        Console.WriteLine("Seeding completed.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}


<<<<<<< HEAD

// Cấu hình Swagger middleware
if (app.Environment.IsDevelopment())



        // Middleware
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>

            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });
        }

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
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();

=======
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
>>>>>>> 88675dc243c071395de510368003d5291069df9e

app.Run();
