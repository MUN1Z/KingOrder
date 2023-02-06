using KingOrder.CrossCutting.IoC;
using KingOrder.Domain.Interfaces;
using Newtonsoft.Json;
using System.Net;

public class Startup
{
    #region properties

    public IConfiguration _configuration { get; }
    public IWebHostEnvironment _environment { get; }

    #endregion

    #region constructors

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    #endregion

    #region public methods implementations

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        NativeInjectorBootStrapper.InjectContext(services, _configuration);
        NativeInjectorBootStrapper.RegisterAutoMapper(services);
        NativeInjectorBootStrapper.RegistereApiBehaviors(services);
        NativeInjectorBootStrapper.RegisterServices(services);

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardLimit = 2;
            options.KnownProxies.Add(IPAddress.Parse("127.0.0.1"));
            options.ForwardedForHeaderName = "X-Forwarded-For-My-Custom-Header-Name";
        });

        services.AddSwaggerGen();

        services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            }
            );
        
        services.AddApplicationInsightsTelemetry(_configuration);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDatabaseManager databaseManager)
    {
        // Configure the HTTP request pipeline.
        //if (env.IsDevelopment())
        //{
        app.UseSwagger();
        app.UseSwaggerUI();
        //}

        app.UseRouting();

        // global cors policy
        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        //if (env.IsDevelopment())
        //{
        databaseManager.SeedData().Wait();
        //}
    }

    #endregion
}
