using KingOrder.Database.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KingOrder.API.IntegrationTests.Factories
{
    public class BaseWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        #region protected methods implementations

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Is be called after the `ConfigureServices` from the Startup
            // which allows you to overwrite the DI with mocked instances
            builder.ConfigureTestServices(services =>
            {
                var descriptorContext =
                    services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<KingOrderContext>));

                services.Remove(descriptorContext);

                services.AddDbContext<KingOrderContext>(optionsBuilder => optionsBuilder.UseInMemoryDatabase("test"));

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var logger = scopedServices
                        .GetRequiredService<ILogger<WebApplicationFactory<Startup>>>();

                    try
                    {
                        var ctx = scope?.ServiceProvider.GetService<KingOrderContext>();

                        ctx.Database.EnsureDeleted();
                        ctx.Database.EnsureCreated();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "", ex.Message);
                    }
                }
            });
        }

        #endregion
    }
}
