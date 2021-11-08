using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Z3._3
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILogger<Startup> logger)
        {
            app.Map("/square", Square =>
            {
                Square.Map("/circle", Circle);
                Square.Map("/rectangle", Rectangle);
            });
            app.Map("/volume", Volume =>
            {
                Volume.Map("/sphere", Sphere);
                Volume.Map("/parallelepiped", Parallelepiped);
            });

            app.Run(async (context) =>
            {
                logger.LogTrace("LogDebug {0}", context.Request.Path);
                logger.LogCritical("LogCritical {0}", context.Request.Path);
                logger.LogDebug("LogDebug {0}", context.Request.Path);
                logger.LogError("LogError {0}", context.Request.Path);
                logger.LogInformation("LogInformation {0}", context.Request.Path);
                logger.LogWarning("LogWarning {0}", context.Request.Path);
                await context.Response.WriteAsync("Page Not Found");
            });
        }
        private static void Circle(IApplicationBuilder app)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            ILogger<Startup> logger = loggerFactory.CreateLogger<Startup>();
            int r = 0;
            double S = 0;
            app.Use(async (context, next) =>
            {
                string u = context.Request.Query["unit"];
                await next();
                context.Response.ContentType = "text/html;charset=utf-8";
                await context.Response.WriteAsync($"<p>Радиус круга: {r}</p>" +
                    $"<p>Площадь круга: {S}{u}</p>"
                   );
                logger.LogTrace("Use - LogDebug {0}", context.Request.Path);
            });
            app.Run(async context =>
            {
                r = Convert.ToInt32(context.Request.Query["r"]);
                S = r * r * Math.PI;
                await Task.FromResult(0);
            });
        }

        private static void Rectangle(IApplicationBuilder app)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            ILogger logger = loggerFactory.CreateLogger<Startup>();
            int width = 0;
            int height = 0;
            double S = 0;
            app.Use(async (context, next) =>
            {
                string u = context.Request.Query["unit"];
                await next();
                context.Response.ContentType = "text/html;charset=utf-8";
                await context.Response.WriteAsync($"<p>Длина прямоугольника: {width}</p>" +
                    $"<p>Ширина прямоугольника: {height}</p>" +
                    $"<pПлощадь прямоугольника: {S}{u} </p>"
                   );
                logger.LogTrace("Use - LogDebug {0}", context.Request.Path);
            }
            );
            app.Run(async context =>
            {
                width = Convert.ToInt32(context.Request.Query["width"]);
                height = Convert.ToInt32(context.Request.Query["height"]);
                S = width * height;
                await Task.FromResult(0);
            });
        }

        private static void Sphere(IApplicationBuilder app)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            ILogger logger = loggerFactory.CreateLogger<Startup>();
            int r = 0;
            double V = 0;
            app.Use(async (context, next) =>
            {
                string u = context.Request.Query["unit"];
                await next();
                context.Response.ContentType = "text/html;charset=utf-8";
                await context.Response.WriteAsync($"<p>Радиус сферы: {r}</p>" +
                    $"<p>Объем сферы: {V}{u}</p>"
                   );
                logger.LogTrace("Use - LogDebug {0}", context.Request.Path);
            });
            app.Run(async context =>
            {
                r = Convert.ToInt32(context.Request.Query["r"]);
                V = r * r * r * Math.PI * 4 / 3;
                await Task.FromResult(0);
            });
        }

        private static void Parallelepiped(IApplicationBuilder app)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            ILogger logger = loggerFactory.CreateLogger<Startup>();
            int width = 0;
            int height = 0;
            int deep = 0;
            double S = 0;
            app.Use(async (context, next) =>
            {
                string u = context.Request.Query["unit"];
                await next();
                context.Response.ContentType = "text/html;charset=utf-8";
                await context.Response.WriteAsync($"<p>Длина параллелепипеда: {width}<p>" +
                    $"<p>Ширина параллелепипеда: {height}</p>" +
                    $"<p>Высота параллелепипеда: {deep}</p>" +
                    $"<p>Объем параллелепипеда: {S}{u}</p>"
                   );
                logger.LogTrace("Use - LogDebug {0}", context.Request.Path);
            });

            app.Run(async context =>
            {
                width = Convert.ToInt32(context.Request.Query["width"]);
                height = Convert.ToInt32(context.Request.Query["height"]);
                deep = Convert.ToInt32(context.Request.Query["deep"]);
                S = width * height * deep;
                await Task.FromResult(0);
            });
        }
    }
}
