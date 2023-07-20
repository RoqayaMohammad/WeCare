using Core.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
             IConfiguration config)
        {
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            //services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            //services.AddScoped<IPhotoService, PhotoService>();
            //services.AddScoped<LogUserActivity>();
            //services.AddSignalR();
            //services.AddSingleton<PresenceTracker>();

            return services;
        }
    }
}
