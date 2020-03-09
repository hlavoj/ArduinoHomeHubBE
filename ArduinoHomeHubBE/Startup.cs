using DataProvider;
using DataProvider.Entities;
using DataProvider.Queries;
using DataProvider.Queries.Interfaces;
using DataProvider.Repositories;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;

namespace ArduinoHomeHubBE
{
    public class Startup
    {

        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["ConnectionString"];
            //var connectionString = @"Server=(localdb)\mssqllocaldb;Database=ArduinoHomeHubFinal;Integrated Security=True";

            var dbContextBuilder = new DbContextOptionsBuilder<AdruinoHomeHubDbContext>();
            dbContextBuilder.UseSqlServer(connectionString);

            services.AddDbContext<AdruinoHomeHubDbContext>(b => b.UseSqlServer(connectionString));


            services.AddScoped<IDateTimeProvider, UtcDateTimeProvider>();
            services.AddScoped(typeof(IUnitOfWorkProvider), provider
                => new EntityFrameworkUnitOfWorkProvider(new AsyncLocalUnitOfWorkRegistry(), ()
                    => new AdruinoHomeHubDbContext(dbContextBuilder.Options)));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AdruinoHomeHubDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // User settings
                options.User.RequireUniqueEmail = true;
                //    //// Password settings
                //    //options.Password.RequireDigit = true;
                //    //options.Password.RequiredLength = 8;
                //    //options.Password.RequireNonAlphanumeric = false;
                //    //options.Password.RequireUppercase = true;
                //    //options.Password.RequireLowercase = false;

                //    //// Lockout settings
                //    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                //    //options.Lockout.MaxFailedAccessAttempts = 10;
            });

            var applicationUrl = Configuration["ApplicationUrl"].TrimEnd('/');

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = applicationUrl;
                    options.SupportedTokens = SupportedTokens.Jwt;
                    options.RequireHttpsMetadata = false; // Note: Set to true in production
                    options.ApiName = IdentityServerConfig.ApiName;
                });

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                .AddInMemoryClients(IdentityServerConfig.GetClients())
                .AddAspNetIdentity<User>();
            //.AddProfileService<ProfileService<User>>();

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(MyAllowSpecificOrigins, builder => { builder.WithOrigins("http://localhost:4200"); });
            //});

            services.AddCors(o => o.AddPolicy(MyAllowSpecificOrigins, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddTransient<IRepository<Light, int>, LightRepository>();
            services.AddTransient<IRepository<TemperatureData, int>, TemperatureRepository>();
            services.AddTransient<IQuery<Light>, LightQuery>();
            services.AddTransient<ITemperatureQuery, TemperatureQuery>();

            services.AddTransient<IDatabaseSeed, DatabaseSeed>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
            //}

            app.UseCors(MyAllowSpecificOrigins);

            // app.UseHttpsRedirection();

            app.UseRouting();



            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
