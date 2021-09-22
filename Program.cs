using LatencyAPI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatencyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureServices((hostContext, services) => {
                    services.AddQuartz(q =>
                    {
                        q.UseMicrosoftDependencyInjectionScopedJobFactory();

                        var jobKey = new JobKey("CollatorJob");
                        q.AddJob<CollatorJob>(opts => opts.WithIdentity(jobKey));

                        q.AddTrigger(opts => opts.ForJob(jobKey).WithIdentity("CollatorJob-Trigger").WithCronSchedule("0/5 * * * * ?"));

                    });
                    services.AddQuartzHostedService(
                        q => q.WaitForJobsToComplete = true
                        );
                });
    }
}
