using TodoAPI.Models;

namespace TodoAPI.Infrastructure
{
    public class DatabaseSeed
    {
        public static async Task SeedDatabaseAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<TodoContext>();

            await context.Activity.AddRangeAsync(
                new Activity { Id = Guid.NewGuid().ToString(), IsComplete = false, Name = "Walk Dog" },
                new Activity { Id = Guid.NewGuid().ToString(), IsComplete = false, Name = "Coffee with friends" },
                new Activity { Id = Guid.NewGuid().ToString(), IsComplete = false, Name = "Laundry" }
            );
            await context.SaveChangesAsync();
        }
    }
}
