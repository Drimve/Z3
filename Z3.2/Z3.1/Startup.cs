using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Z3._1
{
    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("sources.json");

            AppConfiguration = builder.Build();
        }
        public IConfiguration AppConfiguration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            ConnectionPolicySettings cps = new ConnectionPolicySettings();
            AppConfiguration.Bind(cps);
            app.Run(async (context) => {
                context.Response.ContentType = "text/html; charset=utf-8";
                string timeout = $"<p>Время ожидания ответа (секунд): {cps.ResponseWaitingTime}</p>";
                string maxCountConnect = $"<p>Максимальное число попыток подключения: {cps.MaxNumOfConAttempts}</p>";
                string dataSources = "<p>Источники данных:</p>";
                foreach (var source in cps.DataSources)
                {
                    dataSources += $"<p>URL: {source.URL}         Приоритет (число) его использования: {source.PriorityItsUse}</p>";
                }
                await context.Response.WriteAsync($"{timeout}{maxCountConnect}{dataSources}");
            });
        }
    }
}

