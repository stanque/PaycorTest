using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaycorTest.Data.Infrastructure;
using PaycorTest.Service.Infrastructure;
using MediatR;
using AutoMapper;
using PaycorTest.API.Infrastructure;
using PaycorTest.Application.Infrastructure;
using Microsoft.OpenApi.Models;
using PaycorTest.Application.Commands;
using Microsoft.Net.Http.Headers;

namespace PaycorTest.API
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            log4net.Config.XmlConfigurator.Configure(logRepository, new FileInfo("Log4Net.config"));

            services.AddControllers();
            services.AddDataLayer(_configuration);
            services.AddServiceLayer(_configuration);
            services.AddApplicationLayer(_configuration);

            services.AddMediatR(typeof(BaseMediatorHandler).Assembly);
            services.AddAutoMapper
            (
                typeof(AutomapperApiProfile), 
                typeof(AutomapperApplicationProfile), 
                typeof(AutomapperServiceProfile)
            );

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc
                (
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Paycor Test Api",
                        Version = "v1",
                        Description = "API for determining am I an absolute dilettante or not... :)",
                        //TermsOfService = new Uri("https://example.com/terms"),
                        //Contact = new OpenApiContact
                        //{
                        //    Name = "John Walkner",
                        //    Email = "John.Walkner@gmail.com",
                        //    Url = new Uri("https://twitter.com/jwalkner"),
                        //},
                        //License = new OpenApiLicense
                        //{
                        //    Name = "Employee API LICX",
                        //    Url = new Uri("https://example.com/license"),
                        //}
                    }
                );

                var xmlFile = "PaycorTest.Api.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddCors(policy =>
            {
                policy.AddPolicy("CorsPolicy", opt => opt
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            //app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Paycor Test Api v1");
                c.RoutePrefix = "swagger/ui";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
