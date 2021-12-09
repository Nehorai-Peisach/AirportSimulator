using Airport.Backend.Hubs;
using Airport.BLL.Interfaces;
using Airport.BLL.Methods;
using Airport.BLL.Services;
using Airport.DAL.Interfaces;
using Airport.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Airport.Backend
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();

            services.AddCors(options =>
            {
                options.AddPolicy("client", builder =>
                {
                    builder.WithOrigins("http://localhost:3000");
                    //.WithMethods("")
                });

                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            services.AddSingleton<IStationService, StationService>();
            services.AddSingleton<ISimulatorLogic, SimulatorLogic>();
            services.AddSingleton<IMyAirport, MyAirport>();
            services.AddSingleton<IPlaneRepository, PlaneRepository>();
            services.AddSingleton<IStationRepository, StationRepository>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<SimulatorClientHub>("/simulatorClient");
                endpoints.MapHub<SimulatorServerHub>("/simulatorServer");
            });
        }
    }
}
 