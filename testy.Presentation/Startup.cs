using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using testy.Application;
using testy.Application.Common.Interfaces;
using testy.Infrastructure;
using testy.Infrastructure.Persistence;
using testy.Middlewares;
using testy.Presentation.Services;
using Coravel;
using Microsoft.Extensions.Logging;
using Coravel.Queuing.Interfaces;
using System;

namespace testy.Presentation
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Allow CORS
            services.AddCors(options =>
            {
                options.AddPolicy("TestyOrigin",
                      builder => builder.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod());
            });

            services.AddDbContext<TestyDbContext>(
                    x => x.UseSqlServer(Configuration.GetConnectionString("Default"),
                    b => b.MigrationsAssembly(typeof(TestyDbContext).Assembly.GetName().ToString()))
                );

            services.AddQueue();

            services.AddScheduler();

            services.AddApplication(Configuration);
            services.AddInfrastructure(Configuration);
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.RegisterSwagger();
            services.RegisterHealthChecks(Configuration);
            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var provider = app.ApplicationServices;
            provider.ConfigureQueue().LogQueuedTaskProgress(provider.GetService<ILogger<IQueue>>());
          
            // app.UseHttpsRedirection();
            app.UseCors("TestyOrigin");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapControllers();
            });
        }
    }
}
