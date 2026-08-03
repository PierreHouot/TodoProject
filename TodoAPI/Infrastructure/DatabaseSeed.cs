using TodoAPI.Models;

namespace TodoAPI.Infrastructure
{
    public class DatabaseSeed
    {
        public static async Task SeedDatabaseAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ActivityContext>();

            await context.Activity.AddRangeAsync(
                new Activity { Id = Guid.NewGuid().ToString(), Name = "LOTR Marathon", Description = "Annual Lord of the ring marathon with my cousins. Someone did say the Aragorn's fun fact" },
                new Activity { Id = Guid.NewGuid().ToString(), Name = "Coffee with friends", Description="A good moment seeing old friends" }
            );
            await context.SaveChangesAsync();
        }
    }
}
