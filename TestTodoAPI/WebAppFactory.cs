using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;
using TodoAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class WebAppFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(IDbContextOptionsConfiguration<ActivityContext>));

            services.Remove(dbContextDescriptor);

            var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbConnection));

            services.Remove(dbConnectionDescriptor);

            services.AddDbContext<ActivityContext>((container, options) =>
            {
                options.UseInMemoryDatabase("TodoTests");
            });
        });

        builder.UseEnvironment("Development");
    }

    public void ResetDatabase()
    {
        using var scope = Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<ActivityContext>();

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }
}