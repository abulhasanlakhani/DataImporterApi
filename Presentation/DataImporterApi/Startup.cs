using DataImporter.Business;
using DataImporter.Business.Interfaces;
using DataImporter.Business.Services;
using DataImporter.Persistence;
using DataImporterApi.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataImporterApi
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
            services.AddCors(options => options.AddPolicy("MyPolicy", policy => policy.AllowAnyOrigin()));
            
            services.AddMvc(options => 
                {
                    options.Filters.Add<JsonExceptionFilter>();
                    options.Filters.Add<RequireHttpsOrCloseAttribute>();
                });

            services.AddSwaggerDocument();

            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IExtractor, Extractor>();
            services.AddScoped<IMappingService, MappingService>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<ITaxCalculator, TaxCalculator>();

            // Add DbContext using SQL Server Provider
            services.AddDbContext<DataImporterContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DataImporterConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("MyPolicy");
            
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
