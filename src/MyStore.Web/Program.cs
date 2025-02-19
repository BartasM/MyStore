﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace MyStore.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(s => s.AddMvc())
                .Configure(app =>
                {
//                    app.UseRouter(r =>
//                        r.MapGet("hi", async (request, response, data) =>
//                        {
//                            await response.WriteAsync("Hi!");
//                        }));
                })
                .UseSerilog((ctx, cfg) =>
                {
                    cfg.Enrich.FromLogContext()
                        .Enrich.WithProperty("Environment", ctx.HostingEnvironment.EnvironmentName)
                        .Enrich.WithProperty("Application", ctx.HostingEnvironment.ApplicationName)
                        .WriteTo.ColoredConsole()
                        .MinimumLevel.Is(LogEventLevel.Information);
                })
                .UseStartup<Startup>();
    }
}
