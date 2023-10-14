using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using SqliteDAL.DAL;

namespace BlazorSqlite.Services;

public class DatabasePersistenceService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly IDbContextFactory<BlazorContext> _dbContextFactory;
    private IJSObjectReference _module;

    public DatabasePersistenceService(IJSRuntime jsRuntime, IDbContextFactory<BlazorContext> dbContextFactory)
    {
        _jsRuntime = jsRuntime;
        _dbContextFactory = dbContextFactory;
    }

    public async Task InitDatabaseAsync()
    {
        // Request file from OPFS
        _module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "/ContextPersistence.js");
        byte[] byteArray = await _module.InvokeAsync<byte[]>("LoadDataAsync");
        // Create a file in memory
        using var fileMemory = File.Open("blazorschool.db", FileMode.OpenOrCreate);
        fileMemory.Write(byteArray, 0, byteArray.Length);
        fileMemory.Flush();
        // Let EF core does it job
        using var ctx = _dbContextFactory.CreateDbContext();
        ctx.Database.Migrate();
        await PersistDatabaseAsync();
    }

    public async Task PersistDatabaseAsync()
    {
        using var fileMemory = File.Open("blazorschool.db", FileMode.OpenOrCreate);
        byte[] buffer = new byte[fileMemory.Length];
        fileMemory.Read(buffer, 0, buffer.Length);
        await _module.InvokeVoidAsync("PersistDataAsync", buffer);
    }
}
