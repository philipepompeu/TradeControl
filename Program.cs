using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using TradeControl.Controllers;
using TradeControl.Domain.Repository;
using TradeControl.Services;

const int LENGTH_LIMIT_FILE = 5 * 1024 * 1024;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

builder.Services.AddOpenTelemetry().WithMetrics(
    metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation()
            .AddRuntimeInstrumentation()
            .AddPrometheusExporter()
            .AddMeter("TradeControl.Metrics");
    });


builder.Services.AddDbContext<TradeControlDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<ITradeOperationRepository, TradeOperationRepository>();

builder.Services.AddScoped<IAssetsService, AssetsService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITradeOperationService, TradeOperationService>();
builder.Services.AddScoped<IFileDocumentRepository, FileDocumentRepository>();

builder.Services.AddHttpClient<B3Service>();

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = LENGTH_LIMIT_FILE; // 5 MB
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TradeControlDbContext>();
    dbContext.Database.Migrate();
    DataSeeder.Generate(dbContext);
}

app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");
app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.Run();
