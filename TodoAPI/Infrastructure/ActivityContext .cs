using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.Infrastructure
{
    public class ActivityContext: DbContext
    {
        public ActivityContext(DbContextOptions<ActivityContext> options) : base(options) { }
        public DbSet<Activity> Activity { get; set; } = null!;
    }
}
