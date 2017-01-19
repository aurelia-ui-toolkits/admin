using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.MongoDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            var client = new MongoClient(Configuration["Mongo:ConnectionString"]);
            var database = client.GetDatabase(Configuration["Mongo:Database"]);
            services.AddSingleton(database);
            services.AddSingleton<ICategoryRepository, CategoryRepository>();

            services.AddIdentityWithMongoStores(Configuration["Mongo:ConnectionString"])
                .AddDefaultTokenProviders();

            services.AddCors();
            services.AddMvc();

            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityConfig.GetApiResources())
                .AddInMemoryClients(IdentityConfig.GetClients())
                .AddAspNetIdentity<IdentityUser>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // probably need to improve this
            app.UseCors(policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowCredentials();
            });

            app.UseIdentity();
            app.UseIdentityServer();

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = Configuration["url"],
                RequireHttpsMetadata = false,

                ApiName = "api1"
            });

            app.UseMvc();
        }
    }
}
