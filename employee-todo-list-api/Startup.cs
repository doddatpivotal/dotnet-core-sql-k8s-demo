using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Rewrite;
using employee_todo_list_api.Data;
using System.Net;
using Microsoft.EntityFrameworkCore;

// using Steeltoe.Management.Endpoint.Metrics;
using Steeltoe.Management.Tracing;
using OpenTelemetry.Resources;
using System.Collections.Generic;
// using Steeltoe.Management.Kubernetes;
// using Steeltoe.Common.Kubernetes;
// using Steeltoe.Extensions.Configuration.Kubernetes;
// using Steeltoe.Management.Endpoint;


namespace employee_todo_list_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // services.AddPrometheusActuator(Configuration);
            // services.AddMetricsActuator(Configuration);
            // services.AddAllActuators(Configuration);
            services.AddDistributedTracing(Configuration, builder =>
            {
              builder.SetResource(new Resource(new Dictionary<string, object>
                {
                    ["application"] = Configuration["management:tracing:exporter:zipkin:applicationName"],
                    ["cluster"] = Configuration["management:tracing:exporter:zipkin:cluster"],
                })).UseZipkinWithTraceOptions(services);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .WithExposedHeaders("content-disposition")
                    .AllowAnyHeader()
                    .SetPreflightMaxAge(TimeSpan.FromSeconds(3600)));
            });
            services.AddDbContext<EmployeeTodoContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Employee TODO service",
                    Version = "v1",
                    Description = "A simple employee todo list",
                });
            });

            services.AddControllers();

            services.AddAutoMapper(typeof(Startup));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee TODO list");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseRewriter(new RewriteOptions()
                .AddRedirect(@"^dashboard$", "index.html", (int)HttpStatusCode.Redirect)
                .AddRedirect(@"^employees$", "index.html", (int)HttpStatusCode.Redirect));
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapAllActuators();
                endpoints.MapControllers();
            });

            UpdateDatabase(app);

        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using var context = serviceScope.ServiceProvider.GetService<EmployeeTodoContext>();
                context.Database.Migrate();
            }
        }
    }
}
