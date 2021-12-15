using Airport.Backend.Hubs;
using Airport.Backend.Interfaces;
using Airport.Backend.Methods;
using Airport.BLL.Interfaces;
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
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            services.AddSingleton<IStationService, StationService>();
            services.AddSingleton<IPlaneService, PlaneService>();
            services.AddSingleton<ISimulatorLogic, SimulatorLogic>();

            services.AddSingleton<IPlaneRepository, PlaneRepository>();
            services.AddSingleton<IStationRepository, StationRepository>();

            services.AddSingleton<IServerToClient, ServerToClient>();
            services.AddSingleton<IServerToSimulator, ServerToSimulator>();
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
