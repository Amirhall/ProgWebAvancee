using labo.signalr.api.Models;
using Microsoft.EntityFrameworkCore;

namespace labo.signalr.api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UselessTask> UselessTasks { get; set; }
}

