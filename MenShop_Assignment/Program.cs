
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
using MenShop_Assignment.Models.Momo;

using Microsoft.OpenApi.Models;
<<<<<<< HEAD
using MenShop_Assignment.Repositories.OrderRepositories;
using MenShop_Assignment.Mapper.MapperOrder;
using MenShop_Assignment.Repositories.AccountRepository;
using MenShop_Assignment.Repositories.AdminRepositories;
using MenShop_Assignment.Services.Momo;
using MenShop_Assignment.Services.PaymentServices;
using Microsoft.Extensions.DependencyInjection;
using MenShop_Assignment.Repositories.CustomerAddress;
using MenShop_Assignment.Services.VNPay;
=======
using MenShop_Assignment.Services;
>>>>>>> 8344139ed88cf4d7d0ba0bf5505f1f9a0d9bd6d9

var builder = WebApplication.CreateBuilder(args);

// Cấu hình DI
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
<<<<<<< HEAD
builder.Services.AddScoped<MapperCustomerAddress>();
=======
builder.Services.AddScoped<IOrderCustomerRepository, OrderCustomerRepository>();
builder.Services.AddScoped<IStorageRepository, StorageRepository>();
builder.Services.AddScoped<IAutoOrderService, AutoOrderService>();
builder.Services.AddScoped<IOrderCustomerRepository, OrderCustomerRepository>();
builder.Services.AddScoped<OrderMapper>();

// Add services to the container.
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();

>>>>>>> 8344139ed88cf4d7d0ba0bf5505f1f9a0d9bd6d9
builder.Services.AddScoped<ICategoryProductRepository, CategoryProductRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<CategoryProductMapper>();
builder.Services.AddScoped<ProductMapper>();
builder.Services.AddScoped<StorageDetailMapper>(); 
builder.Services.AddScoped<IStorageRepository, StorageRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<OrderMapper>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<ICustomerAddressRepository, CustomerAddressRepository>   ();
builder.Services.AddScoped<IVNPayService, VNPayService>();

// Add Identity + DbContext
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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


// Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    options.CustomSchemaIds(type => type.FullName);
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

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

