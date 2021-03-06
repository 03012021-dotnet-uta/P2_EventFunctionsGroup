using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Repository;
using Repository.Contexts;
using Repository.Repos;

namespace WebApi
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
            // services.AddCors((options) =>
            // {
            //     options.AddPolicy(name: "dev", builder =>
            //     {
            //         //builder.WithOrigins("https://eventsfunctions.azurewebsites.net", "https://eventsfunctionfe.azurewebsites.net/")
            //         builder.AllowAnyOrigin()
            //         .AllowAnyHeader()
            //         .AllowAnyMethod();
            //     });
            // });

            string connectionString = Configuration.GetConnectionString("localdb");
            string testConnectionString = Configuration.GetConnectionString("testdb");

            services.AddDbContext<EventFunctionsContext>(options =>
			{
				if (!options.IsConfigured)
                {
                    options.UseSqlServer(testConnectionString);
                }
			});

            services.AddScoped<UserLogic>();
            services.AddScoped<ManagerLogic>();
            services.AddScoped<EventLogic>();
            services.AddScoped<EventRepo>();
            services.AddScoped<EventTypeRepo>();
            services.AddScoped<LocationRepo>();
            services.AddScoped<ReviewRepo>();
            services.AddScoped<UserRepo>();
            services.AddScoped<UsersEventRepo>();
    
            //services.AddScoped<TestRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));

            app.UseStatusCodePages();

            app.UseHttpsRedirection();

            app.UseRewriter(new RewriteOptions()
                .AddRedirect("^$", "index.html"));

            app.UseStaticFiles();

            app.UseRouting();

            //app.UseCors("dev");//you must have this for cors to work

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
