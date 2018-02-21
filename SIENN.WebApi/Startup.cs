using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore;
using SIENN.DAL;
using Microsoft.Extensions.Logging;
using SIENN.DbAccess.IRepositories;
using SIENN.DbAccess.Repositories;
using SIENN.DbAccess;

namespace SIENN.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection = @"Data Source=DESKTOP-J8EUELK;Initial Catalog=EnlightDb;Integrated Security=True;Connect Timeout=30;";
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "SIENN Recruitment API"
                });
            });

            services.AddMvc();
            //services.AddDbContext<SIENNDbContext>(options => options.UseSqlServer(connection)); 
            services.AddDbContext<SIENNDbContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("SIENN.WebApi")));
            services.AddDbContext<SIENNDbContext>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ITypeRepository, TypeRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SIENNDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            /*
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Units}/{action=Index}/{id?}");
            });
            */



            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SIENN Recruitment API v1");
            });
            
            SIENNDbInitializer.Initialize(context);
            app.UseMvc();
            


        }
    }
}
