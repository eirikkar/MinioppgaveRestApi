using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=Database/database.db")
);
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.MapGet("/", () => "Hello World!");
app.MapPost(
    "api/tasks",
    async (AppDbContext db, Tasks task) =>
    {
        task.Id = Guid.NewGuid();
        await db.Tasks.AddAsync(task);
        await db.SaveChangesAsync();
        return Results.Created($"/tasks/{task.Id}", task);
    }
);
app.MapGet(
    "api/tasks",
    async (AppDbContext db) =>
    {
        var tasks = await db.Tasks.ToListAsync();
        if (tasks == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(tasks);
    }
);
app.MapGet(
    "api/tasks/{id}",
    async (AppDbContext db, Guid id) =>
    {
        var task = await db.Tasks.FindAsync(id);
        if (task == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(task);
    }
);

app.Run();
