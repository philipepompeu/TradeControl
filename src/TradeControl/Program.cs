using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using TradeControl.Configurations;
using TradeControl.Controllers;
using TradeControl.Services;

const int LENGTH_LIMIT_FILE = 5 * 1024 * 1024;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddTelemetry()
    .AddRepositories(builder.Configuration);

builder.Services.AddHostedService<ConsolidatePositionService>();

builder.Services.AddHttpClient<B3Service>();

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = LENGTH_LIMIT_FILE; // 5 MB
});
builder.Services.AddMemoryCache(options =>
{
    options.SizeLimit = 1024; 
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
