using KingOrder.CrossCutting.Http.Filters;
using KingOrder.CrossCutting.IoC.Helpers;
using KingOrder.Database.Contexts;
using KingOrder.Database.UOW;
using KingOrder.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using KingOrder.Database.Seed;

namespace KingOrder.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        #region constants

        private const string _service = "Service";
        private const string _repository = "Repository";
        private const string _profille = "Profile";
        private const string _connection = "DB_CONNECTION_STRING";

        #endregion

        #region public methods impplementations

        public static void InjectContext(IServiceCollection services, IConfiguration configuration)
        {
            var databaseConnectionString = Environment.GetEnvironmentVariable(_connection) ?? configuration.GetConnectionString(_connection);

            //SQLSERVER
            //services.AddDbContext<KingOrderContext>(
            //    optionsBuilder => optionsBuilder.UseSqlServer(databaseConnectionString),
            //    ServiceLifetime.Scoped,
            //    ServiceLifetime.Singleton);

            //INMemory
            services.AddDbContext<KingOrderContext>(optionsBuilder => optionsBuilder.UseInMemoryDatabase("KingOrderContext"));

            services.AddScoped<IDatabaseManager, DatabaseManager>();
        }

        public static void RegisterServices(IServiceCollection services)
        {
            // ASPNET
            services.AddHttpContextAccessor();

            //AUTOMAICALLY SERVICES AND REPOSITORIES
            var scanAssemblies = AssemblyHelper.Instance().GetAllAssemblies();

            var servicesAndRepositories = scanAssemblies
                .SelectMany(o => o.DefinedTypes
                    .Where(x => x.IsInterface)
                    .Where(c => c.FullName.EndsWith(_service) || c.FullName.EndsWith(_repository))
                );

            foreach (var typeInfo in servicesAndRepositories)
            {
                var types = scanAssemblies
                    .SelectMany(o => o.DefinedTypes
                        .Where(x => x.IsClass)
                        .Where(x => typeInfo.IsAssignableFrom(x))
                    );

                foreach (var type in types)
                    services.TryAdd(new ServiceDescriptor(typeInfo, type, ServiceLifetime.Scoped));
            }

            //CONTEXT
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void RegisterAutoMapper(IServiceCollection services)
        {
            var scanAssemblies = AssemblyHelper.Instance().GetAllAssemblies();

            var profiles = scanAssemblies
               .SelectMany(o => o.DefinedTypes
                   .Where(x => x.IsClass)
                   .Where(c => c.FullName.EndsWith(_profille))
               );

            foreach (var profile in profiles)
                services.AddAutoMapper(profile);
        }

        public static void RegistereApiBehaviors(IServiceCollection services)
        {
            services.AddMvcCore().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context => ValidationHelper.GetInvalidModelStateResponse(context);
            });

            services.AddMvcCore(options => options.Filters.Add<ExceptionFilter>());
        }

        #endregion
    }
}
