using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.Data.Entities;
using WebApiProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Entity Framework with MySQL
builder.Services.AddDbContext<YahooFinanceDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("YahooFinanceDb"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("YahooFinanceDb"))
    ));

// Register your existing services
builder.Services.AddScoped<IYahooFinanceService, YahooFinanceService>();
builder.Services.AddScoped<IAwsParameterStoreService, AwsParameterStoreService>();

// Register the cache service
builder.Services.AddScoped<ICacheService, CacheService>();

// Register the cached wrapper service (this will be the one used by the controller)
builder.Services.AddScoped<IYahooFinanceService>(provider =>
{
    var originalService = provider.GetRequiredService<YahooFinanceService>();
    var cacheService = provider.GetRequiredService<ICacheService>();
    var logger = provider.GetRequiredService<ILogger<CachedYahooFinanceService>>();
    
    return new CachedYahooFinanceService(originalService, cacheService, logger);
});

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
