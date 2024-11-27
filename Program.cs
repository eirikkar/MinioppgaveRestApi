using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

var builder = WebApplication.CreateBuilder(args);

// AppdbContext and Sqlite connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=Database/database.db")
);
var app = builder.Build();

// Create the database and run the migrations if it doesn't exist
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

//Health endpoint to test the connection
app.MapGet("/health", () => "Healthy!");

// Post, Get, Put and Delete endpoints
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

app.MapPut(
    "api/tasks/{id}",
    async (AppDbContext db, Guid id, Tasks task) =>
    {
        var currentTask = await db.Tasks.FindAsync(id);
        if (currentTask == null)
        {
            return Results.NotFound();
        }
        currentTask.Title = task.Title;
        currentTask.Description = task.Description;
        currentTask.IsCompleted = task.IsCompleted;
        currentTask.Duedate = task.Duedate;
        await db.SaveChangesAsync();
        return Results.Ok(currentTask);
    }
);
app.MapDelete(
    "api/tasks/{id}",
    async (AppDbContext db, Guid id) =>
    {
        var task = await db.Tasks.FindAsync(id);
        if (task == null)
        {
            return Results.NotFound();
        }
        db.Tasks.Remove(task);
        await db.SaveChangesAsync();
        return Results.Ok($"Task with id: {id} deleted successfully");
    }
);

app.Run();
