using MenShop_Assignment.Datas;
using MenShop_Assignment.Mapper;
using MenShop_Assignment.Models.Momo;
using MenShop_Assignment.Repositories.ColorRepositories;
using MenShop_Assignment.Services.Momo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<GHTKOrderMapper>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Scan(scan => scan
	.FromAssemblyOf<Program>() // hoặc typeof(ProductRepository) nếu ở DLL khác
		.AddClasses(classes => classes.Where(c => c.Name.EndsWith("Repository")))
			.AsImplementedInterfaces()
			.WithScopedLifetime()
		.AddClasses(classes => classes.Where(c => c.Name.EndsWith("Mapper")))
			.AsImplementedInterfaces()
			.WithScopedLifetime()
);




// --------------------------
// Configure Services (DI)
// --------------------------


builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure Momo API
builder.Services.Configure<MomoOptionModel>(
    builder.Configuration.GetSection("MomoAPI"));

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<MomoOptionModel>>().Value);

// --------------------------
// Swagger
// --------------------------
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
		options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
		options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
	});
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
	{
		policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
	});
});


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
}
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
	});
}
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
