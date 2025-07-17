using Microsoft.EntityFrameworkCore;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using TradeControl.Controllers;
using TradeControl.Domain.Repository;
using TradeControl.Services;

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

builder.Services.AddHttpClient<B3Service>();

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
