using VamoPlay.CrossCutting.IoC;
using VamoPlay.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Net;
using Microsoft.OpenApi.Models;
using VamoPlay.CrossCutting.IoC.HttpFilters;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using VamoPlay.CrossCutting.Auth.Constants;
using VamoPlay.CrossCutting.Auth.Handlers;
using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport.Extensions;
using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport.Integrations;

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

        var builder = new ConfigurationBuilder()
            .SetBasePath(environment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();
        _configuration = builder.Build();
    }

    #endregion

    #region public methods implementations

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        NativeInjectorBootStrapper.InjectAuthorization(services);
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

        //services.AddSwaggerGen(c =>
        //{
        //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "VamoPlay.API", Version = "v1" });

        //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        //    {
        //        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        //        Name = "Authorization",
        //        In = ParameterLocation.Header,
        //        Type = SecuritySchemeType.ApiKey,
        //        Scheme = "Bearer"
        //    });

        //    c.AddSecurityRequirement(new OpenApiSecurityRequirement());

        //    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        //        {
        //                {
        //                    new OpenApiSecurityScheme
        //                    {
        //                        Reference = new OpenApiReference
        //                            {
        //                            Type = ReferenceType.SecurityScheme,
        //                            Id = "Bearer"
        //                            },
        //                            Scheme = "oauth2",
        //                            Name = "Bearer",
        //                            In = ParameterLocation.Header,
        //                        },
        //                        new List<string>()
        //                    }
        //        });

        //    c.OperationFilter<SwaggerJsonIgnoreFilter>();
        //});

        //services.AddJsonMultipartFormDataSupport(JsonSerializerChoice.Newtonsoft);

        //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        services
            .AddAuthentication()
            .AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>(AuthenticationConstants.Bearer, null);

        //services.AddAuthentication()
        //        .AddGoogle(options =>
        //        {
        //            options.ClientId = "622299968061-4j01jkhbkg64q9cvsr8l7d13ksjli0vm.apps.googleusercontent.com";
        //            options.ClientSecret = "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";
        //            options.SignInScheme = IdentityConstants.ExternalScheme;
        //        });

        services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            }
            );

        //services.AddApplicationInsightsTelemetry(_configuration);
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

        databaseManager.SeedSuperAdmin().Wait();

        //if (env.IsDevelopment())
        //{
            databaseManager.SeedTestData().Wait();
        //}
    }

    #endregion
}
