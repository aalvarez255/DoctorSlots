using DoctorSlots.Api.Services;
using DoctorSlots.Api.Services.SlotParser;
using DoctorSlots.Api.Services.SlotServiceClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DoctorSlots.Api.Models;
using DoctorSlots.Api.Utils;
using Microsoft.AspNetCore.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Http;
using DoctorSlots.Api.DTOs;

namespace DoctorSlots.Api
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
            services.Configure<SlotServiceConfiguration>(Configuration.GetSection("SlotService"));

            services.AddScoped<IEncoder, Utils.Encoder>();
            services.AddScoped<IAuthHttpClient, SlotHttpClient>();
            services.AddScoped<ISlotService, SlotService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Handle global errors (error 500)
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        var ex = error.Error;
                        await context.Response.WriteAsync(new ApiError(500, ex.Message)
                            .ToString(), Encoding.UTF8);
                    }
                });
            });

            app.UseMvc();
        }
    }
}
