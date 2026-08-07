using Microsoft.EntityFrameworkCore;
using TodoAPI.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("TodoContext")
    ?? throw new InvalidOperationException("Connection string 'TodoContext' not found.");

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ActivityContext>(opt => opt.UseInMemoryDatabase("TodoList"));

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.WithOrigins(["http://localhost:5173", "http://localhost:4000"])
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await DatabaseSeed.SeedDatabaseAsync(app.Services);
    app.MapOpenApi();
    app.UseCors();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

