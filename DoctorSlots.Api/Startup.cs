using DoctorSlots.Api.Services;
using DoctorSlots.Api.Services.SlotService;
using DoctorSlots.Api.Services.SlotService.Models;
using DoctorSlots.Api.Services.SlotService.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddSingleton<IEncoder, Encoder>();
            services.AddScoped<IAuthHttpClient, SlotHttpClient>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
