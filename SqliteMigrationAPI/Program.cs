using Microsoft.EntityFrameworkCore;
using SqliteDAL.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BlazorContext>(dbContextBuilder => dbContextBuilder.UseSqlite("Data Source=MigrateDatabase/blazorschool.db", options =>
options.MigrationsAssembly("SqliteDAL")));
// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

using var scope = app.Services.CreateScope();
var ctx = scope.ServiceProvider.GetRequiredService<BlazorContext>();
ctx.Database.Migrate();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
