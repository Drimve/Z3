using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Z3
{
    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            AppConfiguration = builder.Build();
        }
        // свойство, которое будет хранить конфигурацию
        public IConfiguration AppConfiguration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            var v = new Value();
            AppConfiguration.Bind(v);

            app.Run(async (context) =>
            {
                string word = "<p>Ключи и значения:</p>";

                foreach (KeyValuePair<string, string> keyValue in v.Values)
                {
                    if (keyValue.Key.StartsWith("a"))
                    {
                        word += $"<p>{keyValue.Key} - {keyValue.Value}</p>";
                    }
                }

                await context.Response.WriteAsync($"{word}");
            });
        }
    }
}
