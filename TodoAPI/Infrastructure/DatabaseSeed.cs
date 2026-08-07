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
                new Activity
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Conference at GameDev ",
                    Description = "I talked about team communication efficiency - a lot to say",
                    Date = new DateOnly(2025, 02, 16),
                },
                new Activity
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "First sandcastle since so long",
                    Description = "Great day, we had icecream too",
                    Date = new DateOnly(2024, 08, 06),
                },
                new Activity
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "LOTR Marathon",
                    Description = "Annual Lord of the ring marathon with my cousins. Someone did say the Aragorn's fun fact",
                    Date = new DateOnly(2026, 05, 12),
                },
                new Activity
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Coffee with friends",
                    Description = "A good moment seeing old friends",
                    Date = new DateOnly(2026, 07, 02)
                },
                new Activity
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "New Spiderman out !",
                    Description = "Went to the theater for Spiderman brand new day. Really cool",
                    Date = new DateOnly(2026, 07, 30)
                },
                new Activity
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Hangout with Ted",
                    Description = "We played some arcade at the maul before watching the city lights",
                    Date = new DateOnly(2024, 11, 21)
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
                    Name = "Matrix",
                    Date = new DateOnly(1999, 06, 23)
                },
                new Activity
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Matrix 2",
                    Date = new DateOnly(2003, 05, 15)
                },
                new Activity
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Matrix 3",
                    Date = new DateOnly(2003, 11, 05)
                },
                new Activity
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "The Lord of the Rings: The Fellowship of the Ring",
                    Date = new DateOnly(2001, 12, 19)
                },
                new Activity
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "The Lord of the Rings: The Return of the King",
                    Date = new DateOnly(2003, 12, 17)
                },
                    new Activity
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "The Lord of the Rings: The Two Towers",
                        Date = new DateOnly(2002, 12, 18)
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
