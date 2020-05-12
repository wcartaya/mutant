using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Mutants.Business.IServices;
using Mutants.Business.Mutant;
using Mutants.Business.Services;
using Mutants.DataAccess;
using Mutants.DataAccess.Interfaces;
using Mutants.DataAccess.Repository;
using System;
using System.IO;
using System.Reflection;

namespace Mutants.API
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
            services.AddDbContext<MutantDBContext>(opts => opts.UseSqlServer(Configuration["MyDbConnection"]));
            services.AddControllers();

            services.AddTransient<IMutantService, MutantService>();
            services.AddScoped<IStatsRepository, StatsRepository>();
            services.AddScoped<IDataRepository<Dna>, DnaRepository>();
            services.AddScoped<IDnaValidator,DnaValidator>();

            services.AddMemoryCache();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "1",
                    Contact = new OpenApiContact
                    {
                        Name = "Williams Cartaya",
                        Email = "wcartaya@gmail.com"
                        //,Url = new Uri("http://wcartaya.com")
                    },
                    Description = "Proyecto que detecta si un humano es mutante basándose en su secuencia de ADN",
                    License = new OpenApiLicense
                    {
                        Name = "Licencia GPL",
                        Url = new Uri("https://www.gnu.org/licenses/#GPL")
                    },
                    Title = "Project recruit mutants"
                    //,TermsOfService = new Uri("http://wcartaya.com")
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
            });
        }
    }
}
