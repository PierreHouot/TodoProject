using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.Infrastructure
{
    public class TodoContext: DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }
        public DbSet<Activity> Activity { get; set; } = null!;
    }
}
