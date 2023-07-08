using VamoPlay.CrossCutting.Http.Filters;
using VamoPlay.CrossCutting.IoC.Helpers;
using VamoPlay.Database.Contexts;
using VamoPlay.Database.UOW;
using VamoPlay.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using VamoPlay.Database.Seed;

namespace VamoPlay.CrossCutting.IoC
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
            //services.AddDbContext<VamoPlayContext>(
            //    optionsBuilder => optionsBuilder.UseSqlServer(databaseConnectionString),
            //    ServiceLifetime.Scoped,
            //    ServiceLifetime.Singleton);

            //INMemory
            services.AddDbContext<VamoPlayContext>(optionsBuilder => optionsBuilder.UseInMemoryDatabase("VamoPlayContext"));

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
