using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models; // 导入 OpenApiInfo 和 OpenApiContact 类
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.AspNetCore.SignalR;
using H_Pannel_lib;
namespace HIS_WebApi
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;
        public static H_Pannel_lib.UDP_Class uDP_Class;
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _environment = environment;


            if (_environment.IsDevelopment())
            {
                uDP_Class = new UDP_Class("0.0.0.0", 29500);
                Console.WriteLine("DEBUG模式");
            }
            else
            {
                uDP_Class = new UDP_Class("0.0.0.0", 29600);
                Console.WriteLine("非DEBUG模式");
            }
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .SetIsOriginAllowed(origin => true)
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
           
            });


            services.AddControllers();
            services.AddSignalR();

            services.AddSwaggerGen(c =>
            {
                // API 服務簡介
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Pharmacy Sysytem API",
                    Description = "Pharmacy Sysytem API",
                    //TermsOfService = new Uri("https://igouist.github.io/"),
                    //Contact = new OpenApiContact
                    //{
                    //    Name = "Igouist",
                    //    Email = string.Empty,
                    //    Url = new Uri("https://igouist.github.io/about/"),
                    //}
                });

                // 讀取 XML 檔案產生 API 說明
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
            app.UseCors(builder => 
            {
                builder.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(_ => true).AllowCredentials();
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(); // 啟用CORS
            app.UseAuthorization();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseWebSockets();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHub<ChatHub>("/chatHub");
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }

}
