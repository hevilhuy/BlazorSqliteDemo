using BlazorSqlite;
using BlazorSqlite.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using SqliteDAL.DAL;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<DatabasePersistenceService>();
builder.Services.AddDbContextFactory<BlazorContext>(dbContextBuilder => dbContextBuilder.UseSqlite("Filename=blazorschool.db"));
var host = builder.Build();

var service = host.Services.GetRequiredService<DatabasePersistenceService>();
await service.InitDatabaseAsync();

await host.RunAsync();
