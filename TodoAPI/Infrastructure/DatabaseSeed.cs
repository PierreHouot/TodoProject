using TodoAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TodoAPI.Infrastructure
{
    public class DatabaseSeed
    {
        public static async Task SeedDatabaseAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ActivityContext>();

            await context.Activity.AddRangeAsync(
                new Activity
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "LOTR Marathon",
                    Description = "Annual Lord of the ring marathon with my cousins. Someone did say the Aragorn's fun fact",
                    Date = new DateOnly(2026, 05, 12),
                },

                new Activity { Id = Guid.NewGuid().ToString(),
                    Name = "Coffee with friends",
                    Description = "A good moment seeing old friends",
                    Date = new DateOnly(2026, 07, 02)
                },
                 new Activity
                 {
                     Id = Guid.NewGuid().ToString(),
                     Name = "Nouvel an",
                     Description = "Just in family, with boardgames",
                     Date = new DateOnly(2025, 12, 31)
                 },
                  new Activity
                  {
                      Id = Guid.NewGuid().ToString(),
                      Name = "We adopted Potato",
                      Description = "After the covid, we moved to Orlando and we feeled weird. John had the idea to adopt a dog so we could walk and meet the neighbourhood. So we welcomed Potato, a chihuahua in our Family !",
                      Date = new DateOnly(2021, 03, 07)
                  }
            );
            await context.SaveChangesAsync();
        }
    }
}
