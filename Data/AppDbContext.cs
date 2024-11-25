using Microsoft.EntityFrameworkCore;

namespace TaskManager.Data;

public class AppDbContext : DbContext
{
    public required DbSet<Task> Tasks { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
}
