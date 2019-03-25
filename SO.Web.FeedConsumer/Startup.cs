using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SO.Data;
using SO.Server.FeedConsumer;
using SO.Web.FeedConsumer.Hubs;

namespace SO.Web.FeedConsumer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataServices()
                .AddFeedConsumerServices()
                .AddAutoMapper();
            services.AddHostedService<TimedHostedService>();
            services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
            services.AddCors();
            services.AddSignalR();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(config =>
                config.AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowAnyOrigin()
                      .AllowCredentials());

            app.UseSignalR(routes =>
            {
                routes.MapHub<SportUpdatesHub>("/sportUpdatesHub");
            });

            app.UseMvc();
        }
    }
}
