using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.Infrastructure
{
    public class ActivityContext(DbContextOptions<ActivityContext> options) : DbContext(options)
    {
        public DbSet<Activity> Activity { get; set; } = null!;
    }
}
