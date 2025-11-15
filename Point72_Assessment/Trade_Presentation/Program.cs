using Microsoft.EntityFrameworkCore;
using Trade_Application.Interfaces;
using Trade_Application.Services;
using Trade_Infrastructure.Persistence;
using Trade_Domain.Repositories;
using Trade_Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers
builder.Services.AddControllers();

// Add API Documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Trade & Position API",
        Version = "v1",
        Description = "REST API for managing trades and positions"
    });
});

// Infrastructure Layer - Database
builder.Services.AddDbContext<TradeDbContext>(options =>
    options.UseInMemoryDatabase("TradePositionDB"));

// Infrastructure Layer - Repositories
builder.Services.AddScoped<ITradeRepository, TradeRepository>();

// Application Layer - Services
builder.Services.AddScoped<ITradeService, TradeService>();
builder.Services.AddScoped<IPositionService, PositionService>();

// Add CORS if needed
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
//app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();